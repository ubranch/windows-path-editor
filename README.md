# Windows Path Editor

A tool for managing your PATH environment variable on Windows.

![screenshot](https://raw.github.com/rix0rrr/WindowsPathEditor/master/screenshot.png)

## Why

On Windows you constantly need to edit your PATH — every tool installs to its own `bin` directory — yet the built-in environment editor gives you a single-line textbox to work with. This app fixes that.

## Features

- drag-and-drop reordering of path entries
- conflict detection between directories (wrong exe or dll being loaded)
- one-click removal of broken/bogus entries
- disk scan to find `bin` directories and add them automatically
- UAC-aware (elevates when writing to system PATH)

## Requirements

- Windows 10 or later
- [.NET 8 Desktop Runtime (x86)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Building from source

```
dotnet build WindowsPathEditor/WindowsPathEditor.csproj -c Release
```

The output exe will be in `WindowsPathEditor/bin/Release/net8.0-windows/`.

## .NET 8 port

The original project targeted .NET Framework 4.0 and no longer ran on modern Windows. It was ported to .NET 8 and cleaned up using [Claude Code](https://docs.anthropic.com/en/docs/claude-code):

- migrated from .NET Framework 4.0 to .NET 8 (sdk-style project)
- replaced removed APIs (`Assembly.CodeBase`, `FileIOPermission` CAS)
- replaced Reactive Extensions usage with async/await
- removed unused dependencies and legacy project files

## Credits

Originally created by [Rico Huijbers](https://github.com/rix0rrr).
