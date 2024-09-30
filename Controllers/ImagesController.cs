using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Model.Domain;
using PracticeProject.Model.DTO;
using PracticeProject.Repositories.Interface;

namespace PracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepo imageRepo;

        public ImagesController(IImageRepo imageRepo)
        {
            this.imageRepo = imageRepo;
        }

        //GET : {apibaseurl}/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
           //call image repo to get all image
            var images = await imageRepo.GetAll();

            //convert domain model to DTO
            var response = new List<BlogImageDto>();
            foreach (var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url
                });
            }
            return Ok(response);
        }

        //POST: {apibaseurl}/api/images
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile  file, [FromForm] string fileName, [FromForm] string title)
        {

            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                //File upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                blogImage = await imageRepo.Upload(file, blogImage);

                //Convert Domain model to DTO
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url
                };
                return Ok(blogImage);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }
            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }
    }
}
