using Microsoft.EntityFrameworkCore;
using Shopii.App.Application.Database;
using Shopii.App.Application.Models;
using System.Security.Cryptography;
using System.Text;

namespace Shopii.App.Application.Services.Auth
{
    public class UsersService
    {
        private readonly IDbContextFactory<ShopiDbContext> _factory;

        public UsersService(IDbContextFactory<ShopiDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<User?> FindUserAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Users.FindAsync(userId);
        }

        public async Task<User?> FindUserAsync(string username, string password)
        {
            using var context = _factory.CreateDbContext();
            return await context.Users.FirstOrDefaultAsync(x => x.Email == username && x.Password == password);
        }

        public async Task<User?> FindUserByEmailAsync(string email)
        {
            using var context = _factory.CreateDbContext();
            return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            using var context = _factory.CreateDbContext();
            var addedUser = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var users = await context.Users.ToListAsync();
            if (users.Count == 1)
                await context.UserRoles.AddAsync(new UserRole { RoleId = 2, User = user });
            else
                await context.UserRoles.AddAsync(new UserRole { RoleId = 1, User = user });
            await context.SaveChangesAsync();
            return addedUser.Entity;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Users
                    .ToListAsync();
        }

        public string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}
