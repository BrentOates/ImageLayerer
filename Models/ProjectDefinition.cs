namespace ImageLayerer.API.Models;

/// <summary>
/// Represents the full definition provided to the image preparation service
/// </summary>
public class ProjectDefinition
{
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public Canvas Canvas { get; set; }
    public IEnumerable<Layer> Layers { get; set; }
}
