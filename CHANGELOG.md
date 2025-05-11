# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),  
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [0.2.0] - 2025-05-11

This release adds sorting:

### Changed
- `tsk list` now outputs incomplete tasks first
- 
### Added
- Sorting is possible with the `--sortby` option in `tsk list`
  - `description` or `desc`
  - `location` or `loc`
  - `duedate` or `date`

---

## [0.1.3] - 2025-05-09

### Fixed
- Fixed inability to remove metadata; metadata can now be removed by passing in
  a blank string (`""`) as the argument

---

## [0.1.2] - 2025-05-05

### Fixed
- Fixed `release.yml` workflow permissions

---

## [0.1.1] - 2025-05-05

### Fixed
- Fixed broken Windows release packaging in `v0.1.0`
- Updated GitHub Actions workflow to use cross-platform `dotnet publish` syntax

---

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
