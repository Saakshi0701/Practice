using PracticeProject.Model.Domain;

namespace PracticeProject.Repositories.Interface
{
    public interface IBlogPostRepo
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> UpdateAsync(BlogPost blogpost);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
