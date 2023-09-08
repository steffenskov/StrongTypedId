using System;
using System.Collections.Concurrent;

namespace StrongTypedId.Collections;

internal class LockedConcurrentDictionary<TKey, TValue>
	where TKey : notnull
{
	private readonly ConcurrentDictionary<TKey, TValue> _dictionary = new();
	private readonly object _lock = new();

	public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
	{
		if (!_dictionary.TryGetValue(key, out var result))
		{
			lock (_lock)
			{
				if (!_dictionary.TryGetValue(key, out result))
				{
					_dictionary[key] = result = valueFactory(key);
				}
			}
		}

		return result;
	}
}