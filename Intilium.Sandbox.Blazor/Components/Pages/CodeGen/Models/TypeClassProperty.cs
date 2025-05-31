namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;

/// <summary>
/// Represents a property of the type (class).
/// </summary>
public sealed class TypeClassProperty
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public TypeClassProperty()
    {
    }

    /// <summary>
    /// Constructor which accepts the name of the property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    public TypeClassProperty(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the flag which indicates if the property has a public getter.
    /// </summary>
    public bool HasGetter { get; set; } = true;

    /// <summary>
    /// Gets or sets the flag which indicates if the property has a public setter.
    /// </summary>
    public bool HasSetter { get; set; } = true;

    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The type (class) of the property.
    /// </summary>
    public TypeClass TypeClass { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier of the type (class) to which the property belongs.
    /// </summary>
    public int TypeClassId { get; set; }

    /// <summary>
    /// Gets or sets the type as string, note that this can be a complex type, like a list. Each type detail will be set in another class instance.
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    /// Gets or sets the max length for string values.
    /// </summary>
    public short? MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the description for the property.
    /// </summary>
    public string? Description { get; set; }
}
