using Microsoft.EntityFrameworkCore.Update.Internal;
using PracticeProject.Model.Domain;

namespace PracticeProject.Repositories.Interface
{
    public interface IImageRepo
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);

        Task<IEnumerable<BlogImage>> GetAll();
    }
}
