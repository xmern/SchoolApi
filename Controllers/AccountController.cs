using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Authentication;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(UserManager<AppUser> userManager, IConfiguration configuration) : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost("register-student")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            if (ModelState.IsValid)
            {
                var existedUser = await userManager.FindByNameAsync(model.Email);
                if (existedUser != null)
                {
                    ModelState.AddModelError("", "Email is already taken");
                    return BadRequest(ModelState);
                }
            }
            var user = new AppUser {UserName=model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };

            var result = await userManager.CreateAsync(user, model.Password);
            var roleResult = await userManager.AddToRoleAsync(user, AppRoles.Student);
            if (result.Succeeded && roleResult.Succeeded)
            {
                var token = GenerateTokenAsync(model.Email, user);
                return Ok(new { token });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost("register-teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] RegisterUserDto model)
        {
            if (ModelState.IsValid)
            {
                var existedUser = await userManager.FindByNameAsync(model.Email);
                if (existedUser != null)
                {
                    ModelState.AddModelError("", "Email is already taken");
                    return BadRequest(ModelState);
                }
            }
            var user = new AppUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };

            var result = await userManager.CreateAsync(user, model.Password);
            var roleResult = await userManager.AddToRoleAsync(user, AppRoles.Teacher);
            if (result.Succeeded && roleResult.Succeeded)
            {
                var token = GenerateTokenAsync(model.Email, user);
                return Ok(new { token });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {
            if (ModelState.IsValid)
            {
                var existedUser = await userManager.FindByNameAsync(model.Email);
                if (existedUser != null)
                {
                    ModelState.AddModelError("", "Email is already taken");
                    return BadRequest(ModelState);
                }
            }
            var user = new AppUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };

            var result = await userManager.CreateAsync(user, model.Password);
            var roleResult = await userManager.AddToRoleAsync(user, AppRoles.Teacher);
            if (result.Succeeded && roleResult.Succeeded)
            {
                var token = GenerateTokenAsync(model.Email, user);
                return Ok(new { token });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
        private async Task<string> GenerateTokenAsync(string email, AppUser user)
        {
            var secret = configuration["JwtConfig:secret"];
            var issuer = configuration["JwtConfig:ValidIssuer"];
            var audience = configuration["JwtConfig:ValidAudiences"];
            if (secret is null || issuer is null || audience is null)
            {
                throw new ApplicationException("Jwt is not set in the configuration");
            }
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, email)
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            // Get the secret in the configuration
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);// FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var token = GenerateTokenAsync(model.Email, user);
                        return Ok(new { token });
                    }
                }
                // If the user is not found, display an error message
                ModelState.AddModelError("errors", "Invalid email or password");
            }
            return BadRequest(ModelState);
        }

    }
}
