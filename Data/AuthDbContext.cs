using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace PracticeProject.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "6b462846-df6a-4f1b-a63f-dc38d4e51359";    //Random guid 
            var writerRoleId = "0c78341f-aa60-4168-95b2-a655d85a4235";    //Random guid 

            //Create Reader Role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },

                //Create Writer Role
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }

            };

            //Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUserId = "41c59ec5-0b98-4968-8be0-b4ab583c3f68";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "sakshi",
                Email = "sakshidhoke.com",
                NormalizedEmail = "sakshi@dhoke.com".ToUpper(),
                NormalizedUserName = "sakshi@dhoke.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Sakshi@07");   //Generate password
            
            builder.Entity<IdentityUser>().HasData(admin);

            //Give Role to Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
