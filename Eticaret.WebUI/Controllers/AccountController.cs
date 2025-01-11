using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Eticaret.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //private readonly DatabaseContext _context;

        //public AccountController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<AppUser> _service;

        public AccountController(IService<AppUser> service)
        {
            _service = service;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            AppUser user = await _service.GetAsync(x => x.UserGuid.ToString()
            == HttpContext.User.FindFirst("UserGuid").Value);
            if (user is null)
            {
                return NotFound();
            }

            var model = new UserEditViewModel
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                Surname = user.Surname,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> IndexAsync(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userGuid = HttpContext.User.FindFirst("UserGuid")?.Value;
                    if (string.IsNullOrEmpty(userGuid))
                    {
                        return Unauthorized();
                    }

                    AppUser user = await _service.GetAsync(x => x.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if (user != null)
                    {
                        user.Surname = model.Surname;
                        user.Phone = model.Phone;
                        user.Name = model.Name;
                        user.Password = model.Password;
                        user.Email = model.Email;

                        _service.Update(user);
                        var result = _service.SaveChanges();

                        if (result > 0)
                        {
                            TempData["Message"] = @"<div class='alert alert-success alert-dismissible fade show' role='alert'>
                                <strong>Bilgileriniz başarıyla güncellenmiştir.</strong>
                                <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>
                            </div>";

                            return RedirectToAction(nameof(IndexAsync));
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser account = await _service.GetAllAsync()
                        .FirstOrDefault(x =>
                        x.Email == loginViewModel.Email &&
                        x.Password == loginViewModel.Password &&
                        x.IsActive);

                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş başarısız. Lütfen bilgilerinizi kontrol ediniz.");
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, account.Name),
                            new Claim(ClaimTypes.Role, account.IsAdmin ? "Admin" : "Customer"),
                            new Claim(ClaimTypes.Email, account.Email),
                            new Claim("UserId", account.Id.ToString()),
                            new Claim("UserGuid", account.UserGuid.ToString())
                        };

                        var userIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal userPrincpal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincpal);
                        return RedirectToAction(string.IsNullOrEmpty(loginViewModel.ReturnUrl)?"/":loginViewModel.ReturnUrl);   
                        var userPrincipal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(userPrincipal);

                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hata oluştu: " + ex.Message);
                }
            }

            return View(loginViewModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    appUser.IsAdmin = false;
                    appUser.IsActive = true;
                    await _service.AddAsync(appUser);
                    await _service.SaveChangesAsync();

                    return RedirectToAction(nameof(SignIn));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kayıt işlemi sırasında hata oluştu: " + ex.Message);
                }
            }

            return View(appUser);
        }

        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
    }
}
