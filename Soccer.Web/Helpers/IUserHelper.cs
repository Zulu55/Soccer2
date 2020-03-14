using Microsoft.AspNetCore.Identity;
using Soccer.Common.Enums;
using Soccer.Web.Data.Entities;
using Soccer.Web.Models;
using System;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public interface IUserHelper
    {
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path, UserType userType);

        Task<UserEntity> GetUserAsync(string email);

        Task<UserEntity> GetUserAsync(Guid userId);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);

        Task LogoutAsync();
    }
}
