using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Soccer.Common.Enums;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using Soccer.Web.Helpers;
using Soccer.Web.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Soccer.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;

        public AccountController(
            IUserHelper userHelper,
            IImageHelper imageHelper,
            ICombosHelper combosHelper,
            DataContext context,
            IConfiguration configuration,
            IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _combosHelper = combosHelper;
            _context = context;
            _configuration = configuration;
            _mailHelper = mailHelper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(99),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(User.Identity.Name);
                var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("ChangeUser");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);
            EditUserViewModel model = new EditUserViewModel
            {
                Address = user.Address,
                Document = user.Document,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                Teams = _combosHelper.GetComboTeams(),
                TeamId = user.Team.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.PicturePath;

                if (model.PictureFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.PictureFile, "Users");
                }

                UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);

                user.Document = model.Document;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.PicturePath = path;
                user.Team = await _context.Teams.FindAsync(model.TeamId);

                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }

            model.Teams = _combosHelper.GetComboTeams();
            return View(model);
        }

        public IActionResult Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Teams = _combosHelper.GetComboTeams()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.PictureFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.PictureFile, "Users");
                }

                UserEntity user = await _userHelper.AddUserAsync(model, path, UserType.User);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    model.Teams = _combosHelper.GetComboTeams();
                    return View(model);
                }

                var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                var response = _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                    $"To allow the user, " +
                    $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                if (response.IsSuccess)
                {
                    ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    return View(model);
                }
            }

            model.Teams = _combosHelper.GetComboTeams();
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            UserEntity user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email or password incorrect.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
