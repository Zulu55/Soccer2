using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Soccer.Common.Enums;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using Soccer.Web.Models;
using System;
using System.Threading.Tasks;
using Soccer.Common.Models;

namespace Soccer.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserHelper(
            DataContext context,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<UserEntity> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(UserEntity user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<bool> IsUserInRoleAsync(UserEntity user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserEntity> AddUserAsync(AddUserViewModel model, string path, UserType userType)
        {
            UserEntity userEntity = new UserEntity
            {
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PicturePath = path,
                PhoneNumber = model.PhoneNumber,
                Team = await _context.Teams.FindAsync(model.TeamId),
                UserName = model.Username,
                UserType = userType
            };

            IdentityResult result = await _userManager.CreateAsync(userEntity, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            UserEntity newUser = await GetUserAsync(model.Username);
            await AddUserToRoleAsync(newUser, userEntity.UserType.ToString());
            return newUser;
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserEntity user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<UserEntity> GetUserAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Team)
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }

        public async Task<UserEntity> GetUserAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Team)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserEntity user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<UserEntity> AddUserAsync(FacebookProfile model)
        {
            UserEntity userEntity = new UserEntity
            {
                Address = "...",
                Document = "...",
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PicturePath = model.Picture?.Data?.Url,
                PhoneNumber = "...",
                Team = await _context.Teams.FirstOrDefaultAsync(t => t.Name == "Colombia"),
                UserName = model.Email,
                UserType = UserType.User,
                LoginType = LoginType.Facebook
            };

            IdentityResult result = await _userManager.CreateAsync(userEntity, model.Id);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            UserEntity newUser = await GetUserAsync(model.Email);
            await AddUserToRoleAsync(newUser, userEntity.UserType.ToString());
            return newUser;
        }
    }
}
