# CodeShepherd.Blazor.UI

**A reusable Blazor UI component library.**

This library provides well-structured, customizable UI components built with Blazor for modern web applications.  
Currently, the library includes a Button component, with more components planned and in active development.

---

## Components

### Button

A flexible button component with multiple styles and support for child content.

#### Features

- Supports predefined button types (`Primary`, `Alert`)
- Accepts a `Label` string or arbitrary child content
- Handles click events with `EventCallback`
- CSS class management based on button type

#### Usage example

```razor
@using CodeShepherd.Blazor.UI.Components.Buttons.Button

<Button Label="Click me" Type="ButtonType.Primary" OnClick="@OnButtonClicked" />

@code {
    private void OnButtonClicked()
    {
        // Your click handling logic here
    }
}