using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Model.Domain;
using PracticeProject.Repositories.Interface;

namespace PracticeProject.Repositories.Implementation
{
    public class ImageRepo : IImageRepo
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly PracticeDbContext dbContext;

        public ImageRepo(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            PracticeDbContext dbContext) 
        { 
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            return await dbContext.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {

            //Upload the image to API/images
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();
            //Update the database
            //https://codepulse.com/images/somefilename.jgp
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

            blogImage.Url = urlPath;

            await dbContext.BlogImages.AddAsync(blogImage);
            await dbContext.SaveChangesAsync();

            return blogImage;
        }
    }
}
