namespace ImageLayerer.API.Models;

public class ImageFile
{
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public byte[] Content { get; set; }
}
