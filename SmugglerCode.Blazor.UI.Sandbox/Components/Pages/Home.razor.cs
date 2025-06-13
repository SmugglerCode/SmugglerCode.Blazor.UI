using Microsoft.AspNetCore.Components;

namespace SmugglerCode.Blazor.UI.Sandbox.Components.Pages;
public partial class Home : ComponentBase
{
    private bool _show = true;
    private string _customerName = "John Doe";

    private List<string> _persons = ["Tommy Ceusters", "Suzy van der Aa", "Marie Ceusters"];
    private List<Person> _personInstances = [new Person("Tommy", "Ceusters").AddPhoto("foto1.png").AddAddress("Steenberg 72A", "3460 Bekkevoort"),
                                             new Person("Suzy", "van der Aa").AddPhoto("foto2.png").AddAddress("Bondgenotenlaan 101", "3000 Leuven")];

    private bool _isDynamicSize = false;

    private string CssStyles => CreateCssStyle();

    private int _size = 16;

    private string CreateCssStyle()
    {
        return $"font-size: {_size}px";
    }

    private void Toggle()
    {
        _show = !_show;
        StateHasChanged();
    }

    private void ToggleDynamicSize()
    {
        _isDynamicSize = !_isDynamicSize;
    }

    private void Increment() { _size += 4; }
    private void Decrement() { _size = _size == 4 ? 4 : _size - 4; }
}

public sealed class Person
{
    public string Name { get; private set; }

    public string LastName { get; private set; }

    public Person(string name, string lastName)
    {
        Name = name;
        LastName = lastName;
    }

    public string ImageSource { get; private set; } = "assets/foto1.png";

    public string City { get; private set; } = string.Empty;

    public string Street { get; private set; } = string.Empty;

    public override string ToString()
    {
        return Name + " " + LastName;
    }

    public Person AddPhoto(string name)
    {
        ImageSource = $"assets/{name}";
        return this;
    }

    public Person AddAddress(string street, string city)
    {
        Street = street;
        City = city;

        return this;
    }
}