using BookSmart.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookSmart.Extensions;
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

        public async Task<ActionResult> Index()
        {
            var members = await _unitOfWork.Members.GetMembersWithMembershipTypeAsync();
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
