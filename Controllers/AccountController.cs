using BookSmart.Data;
using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManger;
        RoleManager<IdentityRole> _roleManager;

        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManger = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManger.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login");
            }
            return View(loginModel);
        }

        [HttpGet("Register")]
        public async Task<ActionResult<RegisterViewModel>> Register()
        {
            if (!_roleManager.RoleExistsAsync(Utility.RoleHelper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Utility.RoleHelper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Utility.RoleHelper.Member));
            }
            var membershipTypes = _unitOfWork.MembershipTypes.GetAll().ToList();

            var viewModel = new RegisterViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    Name = registerModel.Name,
                    MembershipTypeId = registerModel.MembershipTypeId
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, registerModel.RoleName);
                    await _signInManger.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerModel);
        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManger.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
