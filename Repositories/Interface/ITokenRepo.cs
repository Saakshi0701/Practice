using Microsoft.AspNetCore.Identity;

namespace PracticeProject.Repositories.Interface
{
    public interface ITokenRepo
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
