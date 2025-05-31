namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;

/// <summary>
/// Represents a parameter of a specific method for a specific type (class).
/// </summary>
public sealed class TypeClassMethodParameter
{
    /// <summary>
    /// /Gets or sets the Id of the method parameter.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the parameter.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the parameter for a specific method.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the type (class) to which the method belongs.
    /// </summary>
    public int TypeId { get; set; }

    public string? Descriptor { get; set; }
}
