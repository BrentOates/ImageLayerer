using ImageLayerer.Application.Interfaces;
using ImageLayerer.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageLayerer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageProjectController : ControllerBase
    {
        private readonly IImageSourceService imageSourceService;
        private readonly IImageDrawingService imageDrawingService;

        public ImageProjectController(IImageSourceService imageSourceService, IImageDrawingService imageDrawingService)
        {
            this.imageSourceService = imageSourceService;
            this.imageDrawingService = imageDrawingService;
        }

        [HttpPost]
        [Route("GetImage")]
        public async Task<FileResult> GetImage([FromBody] ImageSource source)
        {
            var image = await imageSourceService.GetImageAsync(source);
            return File(image.Content, image.MimeType, image.FileName);
        }

        [HttpPost]
        [Route("GenerateImage")]
        public async Task<FileResult> GenerateImage([FromBody] ProjectDefinition project)
        {
            var image = await imageDrawingService.GenerateImage(project);
            return File(image, "image/png");
        }
    }
}