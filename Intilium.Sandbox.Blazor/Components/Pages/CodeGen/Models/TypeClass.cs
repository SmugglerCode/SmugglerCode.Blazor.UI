namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;

/// <summary>
/// Represents a type (class), which represent a c# class.
/// </summary>
public sealed class TypeClass
{
    /// <summary>
    /// Gets or sets the unique identifier for the TypeClass instance.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the Type.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the namespace of the type.
    /// </summary>
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a description, this value is not mandatory.
    /// </summary>
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the flag which indicates if the Type is a primitive.
    /// </summary>
    public bool IsPrimitive { get; set; } = false;

    /// <summary>
    /// Gets or sets the list of properties.
    /// </summary>
    public List<TypeClassProperty> Properties { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of methods.
    /// </summary>
    public List<TypeClassMethod> Methods { get; set; } = [];

    /// <summary>
    /// Gets a boolean value which indicates if the Type has properties.
    /// </summary>
    public bool HasProperties => Properties != null && Properties.Count > 0;

    /// <summary>
    /// Gets a booolean value which indicates if the Type has methods.
    /// </summary>
    public bool HasMethods => Methods != null && Methods.Count > 0;
}
