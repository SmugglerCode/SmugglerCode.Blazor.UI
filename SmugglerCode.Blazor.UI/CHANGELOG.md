# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a changelog](https://keepachangelog.com/en/1.0.0),
and this project adheres to [Semantic Versioning](https://semver.org/).

## [1.0.6] - 2025-06-14

### Fixed

- Sizing issue for the drop down icon

## [1.0.5] - 2025-06-13

### Added
- Initial release of **DropDown<T>** component:
- Generic typing, property binding (`PropertyName`)
- Built-in filter textbox via `ShowFilter`
- Cascading disabled scope and dynamic sizing support
- Keyboard navigation (Ctrl + Z, arrows, Enter)
- Custom `ItemTemplate` rendering
- Accessibility: `role="combobox"` and proper focus management
- JS-interop helper (`smugglercodedropdownhelper.js`) registers/unregisters per instance.

## [1.0.4] - 2025-06-10

### Fixed

- Background color of the input container has been set to white as well

### Added

- Possibility to switch between fixed sizing and dynamic sizing, dynamic sizing is based on the font-size of the first parent which defines a concrete value.
- A DynamicCssScope component which allows us to set the scope for dynamic of fixed sizing of a component.
## [1.0.3] - 2025-06-02

### Fixed

- Set `width: 100%` on `input` inside `TextBox` to ensure consistent sizing inside container.

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
