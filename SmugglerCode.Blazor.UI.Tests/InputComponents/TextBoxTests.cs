using Bunit;
using SmugglerCode.Blazor.UI.Components.Inputs;

namespace SmugglerCode.Blazor.UI.Tests.InputComponents;

public class TextBoxTests
{
    [Fact]
    public void CheckIfTextBoxIsGenerated()
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<TextBox>();

        // assert
        component.Find("div[role = 'textbox']");
        component.Find("input");
    }

    [Fact]
    public void Check_Disabled_state()
    {
        using var context = new TestContext();

        var component = context.RenderComponent<TextBox>(ps =>
            ps.Add(tb => tb.IsDisabled, true)
        );
        var input = component.Find("input");

        component.Find("div.sc-disabled");
        Assert.True(input.HasAttribute("disabled"));
    }

    [Fact]
    public void PressingEnter_CallsOnEnter_WithCurrentText()
    {
        // Arrange
        using var context = new TestContext();
        string? enteredValue = null;

        var component = context.RenderComponent<TextBox>(ps => ps
            .Add(p => p.Text, string.Empty)
            .Add(p => p.OnEnter, (string v) => enteredValue = v)
        );

        var input = component.Find("input");

        // Act
        input.Input("Hello world");
        input.KeyPress(key: "Enter");

        // Assert
        Assert.Equal("Hello world", enteredValue);
    }

    [Fact]
    public void PressingEnter_CallsOnEnter_WhenDisabled()
    {
        // Arrange
        using var context = new TestContext();
        string? enteredValue = null;

        var component = context.RenderComponent<TextBox>(ps => ps
            .Add(p => p.Text, string.Empty)
            .Add(p => p.IsDisabled, true)
            .Add(p => p.OnEnter, (string v) => enteredValue = v)
        );

        var input = component.Find("input");

        // Act
        input.Input("Hello world");
        input.KeyPress(key: "Enter");

        // Assert
        Assert.True(enteredValue == null);
    }

    [Fact]
    public void FixedSizingTest()
    {
        using var context = new TestContext();

        var component = context.RenderComponent<TextBox>();

        component.Find("div.fixed-size");
    }

    [Fact]
    public void DynamicSizingTest()
    {
        using var context = new TestContext();

        var component = context.RenderComponent<TextBox>(ps => ps
            .Add(t => t.IsDynamicSizing, true)
        );

        component.Find("div.dynamic-size");
    }

    [Fact]
    public void TwoWayBinding_Works_BothDirections()
    {
        using var context = new TestContext();

        string currentValue = "initial value";

        var component = context.RenderComponent<TextBox>(ps => ps
            .Bind(p => p.Text, currentValue, newValue => currentValue = newValue!)
        );

        // Act
        var input = component.Find("input");
        Assert.Equal("initial value", input.GetAttribute("value"));

        input.Input("Hello");

        // Assert
        Assert.Equal("Hello", currentValue);
    }
}
