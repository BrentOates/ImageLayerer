using ImageLayerer.Application.Interfaces;
using ImageLayerer.Application.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace ImageLayerer.Application.Services;

public class ImageDrawingService : IImageDrawingService
{
    private readonly IImageSourceService imageSourceService;

    public ImageDrawingService(IImageSourceService imageSourceService)
    {
        this.imageSourceService = imageSourceService ?? throw new ArgumentNullException(nameof(imageSourceService));
    }

    public async Task<byte[]> GenerateImage(ProjectDefinition projectDefinition)
    {
        var background = await imageSourceService.GetImageAsync(projectDefinition.Background);
        Image backgroundImg = Image.Load(background.Content);

        backgroundImg.Mutate(x => x.DrawImage(backgroundImg, 1));
        var layers = projectDefinition.Layers.OrderBy(x => x.LayerIndex);

        foreach (var layer in layers)
        {
            var layerImage = await imageSourceService.GetImageAsync(layer.ImageSource);
            Image layerImg = Image.Load(layerImage.Content);
            layerImg.Mutate(x => x.Resize(layer.Width, layer.Height));

            backgroundImg.Mutate(x => x.DrawImage(layerImg, new Point(layer.PosX, layer.PosY), 1));
        }

        using MemoryStream memoryStream = new();
        backgroundImg.Save(memoryStream, new PngEncoder());
        return memoryStream.ToArray();
    }
}
