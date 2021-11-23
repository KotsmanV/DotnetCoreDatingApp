using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await userRepository.GetUsersAsync();
            var returnedUsers = mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(returnedUsers);
        }

        ////api/users/id
        //[HttpGet("{id}")]
        //public async Task<ActionResult<AppUser>> GetUser(int id)
        //{
        //    var user = await userRepository.GetUserByIdAsync(id);
        //    return user;
        //}

        //api/users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //var returnedUser = await userRepository.GetMemberAsync(username);
            //return Ok(returnedUser);
            return await userRepository.GetMemberAsync(username);
        }
    }
}