# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),  
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [0.1.1] - 2025-05-05

### Fixed
- Fixed broken Windows release packaging in `v0.1.0`
- Updated GitHub Actions workflow to use cross-platform `dotnet publish` syntax

## [0.1.0] - 2025-05-05

### Added
- Initial public release of `tsk`, a fast, flexible command-line todo manager
- Support for plaintext file format (`tsk.txt`) with readable syntax
- Command-line interface built with Spectre.Console.Cli
- Core commands: `add`, `list`, `update`, `complete`, `incomplete`, `delete`
- Aliases for common commands (e.g. `tsk a`, `tsk l`, `tsk x`, `tsk o`, `tsk d`)
- Metadata support: tags, due dates, and locations
- Support for editing the todo file by hand (DSL format)
- Cross-platform support (Linux, macOS, Windows) with prebuilt binaries
- Configurable `--file` flag to specify custom storage location

### Changed
- n/a

### Removed
- n/a
