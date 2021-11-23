using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(DataContext context, IMapper automapper)
        {
            dbContext = context;
            mapper = automapper;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await dbContext.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await dbContext.Users
                .Include(p=>p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await dbContext.Users
                .Where(u => u.UserName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await dbContext.Users
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .ToList();
        }

    }
}
