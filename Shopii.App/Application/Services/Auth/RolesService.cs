﻿using Microsoft.EntityFrameworkCore;
using Shopii.App.Application.Database;
using Shopii.App.Application.Models;
using Shopii.App.Application.Startup;

namespace Shopii.App.Application.Services.Auth
{
    public class RolesService
    {
        private readonly IDbContextFactory<ShopiDbContext> _factory;

        public RolesService(IDbContextFactory<ShopiDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Role>> FindUserRolesAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            var roles = await context.Roles.Where(role => role.UserRoles.Any(x => x.UserId == userId)).ToListAsync();
            return roles;
        }

        public async Task<bool> IsUserInRole(int userId, string roleName)
        {
            using var context = _factory.CreateDbContext();
            var userRolesQuery = from role in context.Roles
                                 where role.Name == roleName
                                 from user in role.UserRoles
                                 where user.UserId == userId
                                 select role;
            var userRole = await userRolesQuery.FirstOrDefaultAsync();
            return userRole != null;
        }

        public async Task<List<User>> FindUsersInRoleAsync(string roleName)
        {
            using var context = _factory.CreateDbContext();
            var roleUserIdsQuery = from role in context.Roles
                                   where role.Name == roleName
                                   from user in role.UserRoles
                                   select user.UserId;
            return await context.Users.Where(user => roleUserIdsQuery.Contains(user.Id))
                .ToListAsync();
        }
    }
}
