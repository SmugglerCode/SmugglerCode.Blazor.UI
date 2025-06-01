# SmugglerCode.Blazor.UI

**A reusable Blazor UI component library.**

This library provides well-structured, customizable UI components built with Blazor for modern web applications.  
Currently, the library includes a `Button` component, with more components planned and actively being developed.

---

## Components

### Button

A flexible button component with multiple styles, interactivity options, and support for child content.

#### Features

- Supports predefined button types (`Primary`, `Alert`)
- Accepts a `Label` string or arbitrary child content
- Handles click events via `EventCallback`
- CSS class management based on button type
- Hover-based coloring for visual feedback
- `IsVisible` flag to show or hide the button (default: `true`)
- `IsDisable` flag to enable or disable the button

#### Usage example

```razor
@using SmugglerCode.Blazor.UI.Components.Buttons

<Button 
    Label="Click me" 
    Type="ButtonType.Primary" 
    IsVisible="true" 
    IsDisable="false"
    OnClick="@OnButtonClicked" />

@code {
    private void OnButtonClicked()
    {
        // Your click handling logic here
    }
}
