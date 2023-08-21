namespace ImageLayerer.Application.Models;

/// <summary>
/// Represents the full definition provided to the image preparation service
/// </summary>
public class ProjectDefinition
{
    public string FileName { get; set; }
    public ImageSource Background { get; set; }
    public IEnumerable<Layer> Layers { get; set; }
}
