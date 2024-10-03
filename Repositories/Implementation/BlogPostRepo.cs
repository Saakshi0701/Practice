using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Model.Domain;
using PracticeProject.Repositories.Interface;

namespace PracticeProject.Repositories.Implementation
{
    public class BlogPostRepo : IBlogPostRepo
    {
        private readonly PracticeDbContext dbContext;

        public BlogPostRepo(PracticeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var exisitingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingBlogPost != null)
            {
                dbContext.BlogPosts.Remove(exisitingBlogPost); 
                await dbContext.SaveChangesAsync();
                return exisitingBlogPost;
            }
            return null;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogpost)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogpost.Id);

            if(existingBlogPost == null)
            {
                return null;
            }

            //Update BlogPost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogpost);

            //Update Categories
            existingBlogPost.Categories = blogpost.Categories;

            await dbContext.SaveChangesAsync();

            return blogpost;
        }

    }
}
