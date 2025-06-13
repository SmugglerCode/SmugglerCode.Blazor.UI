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
- Inherits disabled state from parent components via cascading parameter

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
```

---

### TextBox<TValue>

A generic input component for text-based user input with type safety and event handling.

#### Features

- Supports two-way binding via Text and TextChanged
- Optional OnEnter callback for Enter key handling
- Converts input values from string to the correct TValue
- IsDisabled and inherited cascading IsDisabled support
- IsVisible flag to toggle component visibility
- Applies disabled attribute and styles when disabled
- Switching between dynamic or fixed sizing for the text box

#### Usage example

```razor
@using SmugglerCode.Blazor.UI.Components.Inputs

<TextBox TValue="int"
         Text="@value"
         TextChanged="@((int? val) => value = val)"
         OnEnter="@OnEnterPressed"
         IsDisabled="false"
         IsVisible="true" />

@code {
    private int? value;

    private void OnEnterPressed(int? val)
    {
        Console.WriteLine($"Enter pressed, value: {val}");
    }
}
```

---

#### Usage example dynamic sizing

```razor

@using SmugglerCode.Blazor.UI.Components.Common

<DynamicCssScope IsDynamicSize="_isDynamicSize">

        <TextBox @bind-Text="_customerName"></TextBox>
        <TextBox @bind-Text="_customerLastName"></TextBox>
        ...

</DynamicCssScope>

```

---

## DropDown<T> component

A fully-featured, keyboard-accessible dropdown / combobox for Blazor that supports
filtering, templating and dynamic (cascading) disabled state.

### Key features
* Generic `<DropDown<T>>` works with any type – simple strings or full POCOs
* Optional client-side filtering textbox (`ShowFilter`)
* Property binding via `PropertyName` when `T` is a complex object
* Custom rendering with `ItemTemplate`
* Dynamic sizing and inherited disabled scope
* Full keyboard support and screen-reader friendly ARIA roles
* Zero-JS setup apart from a small helper script

Import only once in app.razor:
<link href="/_content/SmugglerCode.Blazor.UI/smugglercode-font.css" rel="stylesheet" />

### Usage

```razor

@page "/example"

<DropDown TValue="Country"
          Items="Countries"
          SelectedItem="@selected"
          SelectedItemChanged="OnCountryChanged"
          PropertyName="Name"
          ShowFilter />

@code {
    private List<Country> Countries = CountryService.All;
    private Country? selected;

    private void OnCountryChanged(Country? value) => selected = value;
}
```

---