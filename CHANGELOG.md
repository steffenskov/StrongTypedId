# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.5.2] - 2026-01-29

### Fixed

- StrongTypedEnum allowed numbers as string in its constructor, thereby storing the value internally as e.g. "0". This in turn would be the value used in serialization, which could cause problems downstream.
    - The fix ensures the internal string representation is always the enum's name, not the number.

## [2.5.1] - 2025-10-15

### Added

- Added changelog to the repo.

## [2.5.0] - 2025-10-15

### Added

- Support for LiteDB via the [StrongTypedId.LiteDB](https://www.nuget.org/packages/StrongTypedId.LiteDB) package. 