using PracticeProject.Model.Domain;

namespace PracticeProject.Repositories.Interface
{
    public interface ICategoryRepo
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetById(Guid id);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);

    }
}
