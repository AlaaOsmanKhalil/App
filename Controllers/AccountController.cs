using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _Context;

        public ITokenService _TokenService;

        public AccountController(DataContext Context, ITokenService tokenService) 
        {
            _Context= Context;
            _TokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDtos registerDto)
        {
            if(await UserExists(registerDto.UserName))
            {
                return BadRequest("Username is taken");
            }

            if(string.IsNullOrWhiteSpace(registerDto.UserName))
            {
                return BadRequest("Please enter username");
            }
            using var hmac = new HMACSHA512(); //for hashing algo for password

            var user = new AppUser
            {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.PassWord)),// ass the password arg is string not bytes
                PasswordSalt = hmac.Key
            };
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();

            return new UserDto
            {
                UserName=user.UserName,
                Token = _TokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _Context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if(user == null)
            {
                return Unauthorized("Invalid Username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt); // get the password using passwordSalt

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.PassWord));// compare this password with the original created one

            for (int i = 0; i< computedHash.Length; i++) 
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                UserName = user.UserName,
                Token = _TokenService.CreateToken(user)
            }; 
        }

        private async Task<bool> UserExists (string username)
        {
            return await _Context.Users.AnyAsync(x => x.UserName== username.ToLower());
        }
    }
}
