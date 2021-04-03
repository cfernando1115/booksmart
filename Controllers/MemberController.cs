using BookSmart.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
