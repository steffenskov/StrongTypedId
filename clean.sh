#!/bin/sh

find . -type d \( -name bin -o -name obj \) -prune -exec rm -rf {} +
dotnet clean
