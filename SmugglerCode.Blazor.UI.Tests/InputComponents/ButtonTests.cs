using Bunit;
using Microsoft.AspNetCore.Components;
using SmugglerCode.Blazor.UI.Components.Buttons;

namespace SmugglerCode.Blazor.UI.Tests.InputComponents;

/// <summary>
/// Unit tests for the button component.
///
/// Tested:
///   - Whether the button is rendered
///   - Whether the 'primary' class is applied
///   - Whether the 'alert' class is applied
///   - Whether the button is not rendered when IsVisible = false
///   - Whether the button has the 'disabled' class in its class list
///   - Whether the button triggers OnClick when enabled, and not when disabled
///   - Whether the content is set when using the Label parameter or setting the content between the Button tags.
/// </summary>
public class ButtonTests
{
    [Fact]
    public void CheckIfButtonIsGenerated()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var renderComponent = ctx.RenderComponent<Button>();

        // Assert
        renderComponent.Find("div[role = 'button']");
    }

    /// <summary>
    /// This will check if the button is a primary button, or an alert button.
    /// </summary>
    [Fact]
    public void CheckTypeClasses()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var renderComponent = ctx.RenderComponent<Button>();
        var renderComponent2 = ctx.RenderComponent<Button>(parameters =>
        {
            parameters.Add(b => b.Type, ButtonType.Alert);
        });

        // Assert
        renderComponent.Find("div[role = 'button'].sc-button-primary");
        renderComponent2.Find("div[role = 'button'].sc-button-alert");
    }

    [Fact]
    public void ShouldNotRenderButtonWhenIsVisibleIsFalse()
    {
        // Arrange
        using var ctx = new TestContext();
        var component = ctx.RenderComponent<Button>(parameters => parameters
            .Add(p => p.IsVisible, false)
        );

        // Act
        var divs = component.FindAll("div");

        // Assert
        Assert.Empty(divs);
    }

    [Fact]
    public void CheckForDisabledButton()
    {
        // Arrange
        using var ctx = new TestContext();

        // act
        var component = ctx.RenderComponent<Button>(parameters => parameters.Add(b => b.IsDisabled, true));

        // assert
        component.Find("div[role = button].sc-button-primary");
    }

    [Fact]
    public void ShouldInvokeOnClickWhenNotDisabled()
    {
        using var ctx = new TestContext();
        bool clicked = false;

        var component = ctx.RenderComponent<Button>(parameters => parameters
            .Add(p => p.IsDisabled, false)
            .Add(p => p.OnClick, EventCallback.Factory.Create(this, () => clicked = true))
        );

        var div = component.Find("div");

        div.Click(); // simulatie van user-click

        Assert.True(clicked); // bevestigt dat OnClick niet werd aangeroepen
    }

    [Fact]
    public void ShouldNotInvokeOnClickWhenDisabled()
    {
        using var ctx = new TestContext();
        bool clicked = false;

        var component = ctx.RenderComponent<Button>(parameters => parameters
            .Add(p => p.IsDisabled, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create(this, () => clicked = true))
        );

        var div = component.Find("div");

        div.Click(); // simulatie van user-click

        Assert.False(clicked); // bevestigt dat OnClick niet werd aangeroepen
    }

    [Fact]
    public void ShouldRenderLabelWhenLabelIsSet()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var component = ctx.RenderComponent<SmugglerCode.Blazor.UI.Components.Buttons.Button>(parameters => parameters
            .Add(p => p.Label, "Click me")
        );

        var div = component.Find("div");

        // Assert
        Assert.Equal("Click me", div.TextContent);
    }

    [Fact]
    public void ShouldRenderChildContentWhenLabelIsEmpty()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var component = ctx.RenderComponent<SmugglerCode.Blazor.UI.Components.Buttons.Button>(parameters => parameters
            .AddChildContent("<span>abc</span>")
        );

        var span = component.Find("span");

        // Assert
        Assert.Equal("abc", span.TextContent);
    }
}
