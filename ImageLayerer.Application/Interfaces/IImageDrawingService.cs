using ImageLayerer.Application.Models;

namespace ImageLayerer.Application.Interfaces;

public interface IImageDrawingService
{
    Task<byte[]> GenerateImage(ProjectDefinition projectDefinition);
}
