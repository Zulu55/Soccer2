﻿using Microsoft.AspNetCore.Identity;
using Soccer.Common.Enums;
using Soccer.Web.Data.Entities;
using Soccer.Web.Models;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path, UserType userType);

        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
