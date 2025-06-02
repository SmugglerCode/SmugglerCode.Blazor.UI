# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a changelog](https://keepachangelog.com/en/1.0.0),
and this project adheres to [Semantic Versioning](https://semver.org/).

## [1.0.2] - 2025-06-02

### Added

- Support for inherited disabled state via `[CascadingParameter(Name = "IsDisabled")]`.
- New generic `TextBox<TValue>` input component:
  - Supports two-way binding via `Text` and `TextChanged`.
  - Handles `Enter` keypress via `OnEnter` callback.
  - Applies `disabled` attribute and gray styling when disabled.
  - Includes fallback CSS styling with inline-block layout and focus behavior.
  - Properly converts string input to `TValue` via `Convert.ChangeType`.

### Removed

- The `Label` parameter from `TextBox<TValue>` was removed.
  Labels are now expected to be provided via a separate `InputGroup` or wrapper component.

## [1.0.1] - 2025-06-02

- Added hover effect with button coloring.
- Added `IsVisible` flag to show or hide the button (default: `true`).
- Added `IsDisable` flag to enable or disable the button.
 
## [1.0.0] - 2025-05-31

### Added

- First public release of the `SmugglerCode.UI` library on NuGet.
- Introduced the "Button" component.
