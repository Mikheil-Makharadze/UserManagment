using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagment.API.DTO;
using UserManagment.Core.Entities;
using UserManagment.Core.Interfaces;

namespace UserManagment.API.Controllers
{
    public class AuthController : BaseAPIController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return BadRequest("Email or Password is incorrect");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest("Email or Password is incorrect");
            }

            user.IsActive = true;
            await userManager.UpdateAsync(user);

            var userInfo = new UserDTO
            {
                Email = user.UserName,
                UserName = user.UserName,
                Token = await tokenService.CreateToken(user)
            };

            return Ok(userInfo);
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            var email = await userManager.FindByEmailAsync(registerDto.Email);

            if (email != null)
            {
                return BadRequest("Account with this Email already exists");
            }

            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(errorMessages);
            }

            var userInfo = new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await tokenService.CreateToken(user)
            };

            return Ok(userInfo);
        }
    }
}
