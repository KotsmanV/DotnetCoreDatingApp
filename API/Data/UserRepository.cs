﻿using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dbContext;
        public UserRepository(DataContext context)
        {
            dbContext = context;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
        }
    }
}
