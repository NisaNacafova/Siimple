using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Siimple.Models;
using Siimple.ViewModels.AccountVm;
using System.Net.Mail;
using System.Net;
using Siimple.Services.Concrets;
using Siimple.Services.Abstracts;

namespace Siimple.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; 
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailConfirm _emailconfirm;
        public AccountController(UserManager<AppUser> user, SignInManager<AppUser> sign, RoleManager<IdentityRole> roleManager,IEmailConfirm email)
        {
            _userManager = user;
            _signInManager = sign;
            _roleManager = roleManager;
            _emailconfirm = email;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            AppUser user = new AppUser()
            {
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.Username,
                Email = register.Email,
            };
            IdentityResult result=await _userManager.CreateAsync(user,register.Password);
            if(!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(register);
                }
            }
            
            //IdentityResult role = await _userManager.AddToRoleAsync(user, "Admin");
            //if (!role.Succeeded)
            //{
            //    foreach (IdentityError error in role.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description);
            //        return View(register);
            //    }
            //}
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("ConfirmUser", "Account", new { token = token, email = user.Email });
            _emailconfirm.SendMessage($"<a href=\"{link}\"> click for email confirmation</a>", "Confirmation link", register.Email);
            //MailMessage message = new MailMessage("7lz9g6x@code.edu.az", user.Email)
            //{
            //    Subject = "Confirmation Email",
            //    Body = link,
            //};
            //SmtpClient smtpClient = new SmtpClient()
            //{
            //    Host = "smtp.gmail.com",
            //    EnableSsl = true,
            //    Port = 587,
            //    Credentials = new NetworkCredential("7lz9g6x@code.edu.az", "aoyhmyjwzdiprgyq")
            //};
            //smtpClient.Send(message);
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            AppUser? user= await _userManager.FindByNameAsync(login.UserName);
            if (user==null)
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return View(login);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password,true,true);
            if(! result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return View(login);
            }
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole()
        //    {
        //        Name = "Admin"
        //    };
        //    await _roleManager.CreateAsync(role);
        //    return Json("Ok");
        //}
        public async Task<IActionResult> ConfirmUser(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email confirm incorrect");
                return View();
            }
            return View();
        }
    }
}
