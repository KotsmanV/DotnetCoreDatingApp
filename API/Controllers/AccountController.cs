using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext dbContext;
        private readonly ITokenService tokenService;

        public AccountController(DataContext dbContext, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.dbContext = dbContext;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username)) return BadRequest("Username already exists!");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            UserDto userDto = new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
            return userDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await dbContext.Users
                .SingleOrDefaultAsync(u=>u.UserName.ToLower() == loginDto.Username.ToLower());

            if(user is null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            UserDto userDto = new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
            return userDto;
        }


        private async Task<bool> UserExists(string username)
        {
            bool userExists = await dbContext.Users.AnyAsync(u=>u.UserName == username.ToLower());
            return userExists;
        }

    }
}