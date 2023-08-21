namespace ImageLayerer.Application.Models;

/// <summary>
/// Represents the background canvas on which layers are added onto.
/// </summary>
public class Canvas : Layer
{
    public long Width { get; set; }
    public long Height { get; set; }
}
