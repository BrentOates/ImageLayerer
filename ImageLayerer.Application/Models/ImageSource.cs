using ImageLayerer.Application.Constants;
using System.Security.Cryptography;
using System.Text;

namespace ImageLayerer.Application.Models;
/// <summary>
/// Represents an image source, which may either be locally sourced or from a remote URL.
/// </summary>
public class ImageSource
{
    public SourceTypes SourceType { get; set; }
    public string SourcePath { get; set; }
    public bool Refresh { get; set; }

    public string CalculateHash()
    {
        byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(SourceType + SourcePath));
        string hashString = BitConverter.ToString(hashBytes).Replace("-", "");
        return hashString;
    }
}
