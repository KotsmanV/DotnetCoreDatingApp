using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            //If the DB exists and has entries, the method stops.
            if (await context.Users.AnyAsync()) return;

            //Read the Json which contains objects that will be mapped to the C# classes
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            //The Json data is deserialized here as AppUsers
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            //The users are added to the DB and have a username and password created as well
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;

                context.Add(user);
            }
            await context.SaveChangesAsync();
        }



    }
}
