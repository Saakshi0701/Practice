using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Model.DTO;
using PracticeProject.Repositories.Interface;

namespace PracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepo tokenRepo;

        public AuthController(UserManager<IdentityUser> userManager,
            ITokenRepo tokenRepo)
        {
            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
        }

        //POST: {apibaseurl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto request)
        {
            //Find the user by email
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {
                //Check if password is correct  
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    //Get the roles assigned to the user
                    var roles = await userManager.GetRolesAsync(identityUser);

                        //Create a Token and Response 
                        var jwtToken = tokenRepo.CreateJwtToken(identityUser, roles.ToList());

                        var response = new LoginResponseDto()
                        {
                            Email = request.Email,
                            Roles = roles.ToList(),
                            Token = jwtToken
                        };

                        return Ok(response);
                    
                }
                ModelState.AddModelError("", "Email or Password Incorrect");
                return ValidationProblem(ModelState);
            }
            return NotFound("User not found"); //*user not found
        }




        //POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterReqDto request)
        {
            //Create IdentityUser Object 
            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };

            //Create user
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                //Add Role to user i.e Reader
                identityResult = await userManager.AddToRoleAsync(user, "Reader");

                //identityResult = await userManager.AddToRoleAsync(user, request.Role); //*for any roles

                if (identityResult.Succeeded)
                {
                    return Ok("Registered Successfully");
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
            }

            return ValidationProblem(ModelState);
        }
    }
}
