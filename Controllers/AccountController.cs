using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.ViewModels;
using BookSmart.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BookSmart.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManger;
        RoleManager<AppRole> _roleManager;

        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<AppRole> roleManager)
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
                    var user = await _userManager.FindByEmailAsync(loginModel.Email);

                    if (await _userManager.IsInRoleAsync(user, Utility.RoleHelper.Member))
                    {
                        return RedirectToAction("Featured", "Book");
                    }

                    return RedirectToAction("Index", "Member");
                }

                ModelState.AddModelError("", "Invalid login");
            }
            return View(loginModel);
        }

        [HttpGet("Register")]
        public ActionResult<RegisterViewModel> Register()
        {
            var viewModel = new RegisterViewModel
            {
                MembershipTypes = _unitOfWork.MembershipTypes.GetAll().ToList()
            };

            return View(viewModel);
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user;
                if(registerModel.RoleName == Utility.RoleHelper.Member)
                {
                    var membershipType = _unitOfWork.MembershipTypes.Get(registerModel.MembershipTypeId);
                    user = new Member
                    {
                        UserName = registerModel.Email,
                        Email = registerModel.Email,
                        Name = registerModel.Name,
                        MembershipTypeId = registerModel.MembershipTypeId,
                        BooksRemaining = membershipType.BooksPerYear
                    };
                }
                else
                {
                    user = new ApplicationUser
                    {
                        UserName = registerModel.Email,
                        Email = registerModel.Email,
                        Name = registerModel.Name
                    };
                }


                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, registerModel.RoleName);
                    if (!User.IsInRole(Utility.RoleHelper.Admin))
                    {
                        await _signInManger.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        TempData["newAdminSignUp"] = user.Name;
                    }

                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            registerModel.MembershipTypes = _unitOfWork.MembershipTypes.GetAll().ToList();
            return View(registerModel);
        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            if (User.IsInRole(Utility.RoleHelper.Member))
            {
                var member = await _unitOfWork.MemberService.GetMemberByUsernameAsync(User.GetUsername());
                member.LastLogin = DateTime.Today;

                await _unitOfWork.CompleteAsync();
            }
            await _signInManger.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
