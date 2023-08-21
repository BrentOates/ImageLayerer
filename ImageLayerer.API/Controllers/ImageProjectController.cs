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

        public ImageProjectController(IImageSourceService imageSourceService)
        {
            this.imageSourceService = imageSourceService;
        }

        [HttpPost]
        [Route("GetImage")]
        public async Task<FileResult> GetImage([FromBody] ImageSource source)
        {
            var image = await imageSourceService.GetImageAsync(source);
            return File(image.Content, image.MimeType, image.FileName);
        }
    }
}