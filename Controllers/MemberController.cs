using BookSmart.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookSmart.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSmart.Models;
using BookSmart.ViewModels;

namespace BookSmart.Controllers
{
    [Route("Member")]
    public class MemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var members = _unitOfWork.Members.GetAll().ToList();
            return View(members);
        }

        [HttpGet("Bag")]
        public async Task<ActionResult> Bag()
        {
            var member = await _unitOfWork.Members.GetMemberByUsernameWithBooksAsync(User.GetUsername());
            if(member != null)
            {
                return View(member);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
