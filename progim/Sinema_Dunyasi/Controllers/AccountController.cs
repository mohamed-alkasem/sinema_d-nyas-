using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinema_Dunyasi.Models;
using Sinema_Dunyasi.ViewModels;

namespace Sinema_Dunyasi.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        //-----------------Account Login--------------------------------------------------//
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "E-posta veya şifre yanlış.");
                    return View(model);
                }
            }
            return View(model);
        }


        //-----------------Account Register--------------------------------------------------//

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser users = new AppUser
                {
                    FullName = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }


        //-----------------Account VerifyEmail--------------------------------------------------//

        public IActionResult VerifyEmail()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var normalizedEmail = model.Email.Trim().ToLower();
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

                if (user == null)
                {
                    ModelState.AddModelError("", $"E-posta adresi bulunamadı: {model.Email}");
                    return View(model);
                }

                return RedirectToAction("ChangePassword", "Account", new { username = user.Email });
            }

            return View(model);
        }


        //-----------------Account ChangePassword--------------------------------------------------//

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Normalize the email before searching
                var normalizedEmail = model.Email.Trim().ToLower();
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

                if (user != null)
                {
                    var result = await _userManager.RemovePasswordAsync(user);

                    if (result.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. Please try again.");
            }
            return View(model);
        }


        //-----------------Account LogOut--------------------------------------------------//

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", null);
        }


        //-----------------Account AdminGiris--------------------------------------------------//

        public IActionResult AdminGiris()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminGiris(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password!, true, false);
                    if (result.Succeeded && await _signInManager.UserManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Adminler" });
                    }
                    ModelState.AddModelError("", "Bu kullanıcı bir admin değil veya giriş bilgileri hatalı.");
                }
                else
                {
                    ModelState.AddModelError("", "E-posta veya şifre hatalı.");
                }
            }
            return View(model);
        }


        //------------------------------------------------------------------------------------//
    }
}
