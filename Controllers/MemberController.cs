using BookSmart.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookSmart.Extensions;
using System.Linq;
using System.Threading.Tasks;
using BookSmart.Models;
using BookSmart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using BookSmart.Utility;
using System;
using Microsoft.Extensions.Configuration;

namespace BookSmart.Controllers
{
    [Authorize]
    [Route("Member")]
    public class MemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMemberService _memberService;

        public MemberController(IUnitOfWork unitOfWork, IConfiguration config, IMemberService memberService)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _memberService = memberService;
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<Member>>> Index(int? pageNumber, int? pageSize, string memberFilter)
        {
            var memberParams = new MemberParams
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? Convert.ToInt32(_config.GetValue<string>("MemberPagination:PageSize")),
                Filter = memberFilter ?? "none"
            };

            var memberFilterViewModel = new MemberFilterViewModel
            {
                Members = await _unitOfWork.Members.GetMembersWithMembershipTypeAsync(memberParams),
                MemberFilters = Utility.MemberFilters.GetMemberFilters()
            };

            return View(memberFilterViewModel);
        }

        [HttpGet("Bag")]
        public async Task<ActionResult<MemberBagViewModel>> Bag()
        {
            var member = await _memberService.GetMemberByUsernameWithBooksAndShipmentsAsync(User.GetUsername());
            if(member != null)
            {
                return View(_memberService.BuildMemberBag(member));
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Update/{id?}")]
        public async Task<ActionResult<MemberUpdateViewModel>> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var member = await _unitOfWork.Members.GetMemberWithMembershipTypeAsync((int)id);

            if (member == null)
            {
                return NotFound();
            }

            var membershipTypes = _unitOfWork.MembershipTypes.GetAll().ToList();

            var viewModel = new MemberUpdateViewModel
            {
                Member = member,
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Update/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(MemberUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Member updatedMember = viewModel.Member;

                var member = await _unitOfWork.Members.GetMemberWithMembershipTypeAsync(updatedMember.Id);

                member.Name = updatedMember.Name;
                member.MembershipTypeId = updatedMember.MembershipTypeId;
                member.BooksRemaining = updatedMember.BooksRemaining;

                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Delete/{id?}")]
        public async Task<ActionResult<Member>> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var member = await _unitOfWork.Members.GetMemberWithMembershipTypeAsync((int)id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Delete/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMember(int? id)
        {
            var member = _unitOfWork.Members.Get(id);

            if (member == null)
            {
                return NotFound();
            }

            _unitOfWork.Members.Remove(member);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }
    }
}
