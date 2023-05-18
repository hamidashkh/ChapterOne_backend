using ChapterOne.Areas.Manage.ViewModels.AccountVMs;
using ChapterOne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ChapterOne.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)  return View();

            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                Surname = registerVM.Surname,
                FatherName = registerVM.FatherName,
                UserName = registerVM.UserName,
            };

            if (await _userManager.Users.AnyAsync(s => s.NormalizedEmail == registerVM.Email.ToUpperInvariant().Trim()))
            {
                ModelState.AddModelError("Email", $"{registerVM.Email} Artiq movcuddur");
                return View(registerVM);
            }
            if (await _userManager.Users.AnyAsync(s => s.NormalizedUserName == registerVM.UserName.ToUpperInvariant().Trim()))
            {
                ModelState.AddModelError("UserName", $"{registerVM.UserName} Artiq movcuddur");
                return View(registerVM);
            }

            IdentityResult identityResult= await _userManager.CreateAsync(appUser,registerVM.Password);
            if (!identityResult.Succeeded) 
            {
                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                    return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser, "Admin");
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Email ve ya Sifre Yanlisdir");
                return View(loginVM);
            }

            

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager
                .PasswordSignInAsync(appUser, loginVM.Password, loginVM.RemindMe, true);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email ve ya Sifre Yanlisdir");
                return View(loginVM);
            }

           

            return RedirectToAction("index", "dashboard", new { areas = "manage" });
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            return View();
        }
        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));


        //    return Content("Ugurlu");
        //}

        //[HttpGet]
        //public async Task<IActionResult> CreateUser()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        Name = "Super",
        //        Surname = "SuperAdmin",
        //        FatherName = "father",
        //        UserName = "SuperAdmin",
        //        Email = "superadmin@gmail.com "


        //    };
        //    await _userManager.CreateAsync(appUser, "SuperAdmin123");
        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Content("U");
        //}
    }
}
