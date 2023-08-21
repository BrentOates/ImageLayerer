namespace ImageLayerer.Application.Models;

/// <summary>
/// Represents one image layer which will be overlayed onto the background and on top of lower indexed layers.
/// </summary>
public class Layer
{
    public int LayerIndex { get; set; }
    public ImageSource ImageSource { get; set; }
}
