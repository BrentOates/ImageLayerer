using ImageLayerer.API.Constants;

namespace ImageLayerer.API.Models;

/// <summary>
/// Represents an image source, which may either be locally sourced or from a remote URL.
/// </summary>
public class ImageSource
{
    public SourceTypes SourceType { get; set; }
    public string SourcePath { get; set; }
    public bool Refresh { get; set; }
}
