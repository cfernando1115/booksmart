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
            var members = await _unitOfWork.MemberService.GetMembersWithMembershipTypeAsync();
            return View(members);
        }

        [HttpGet("Bag")]
        public async Task<ActionResult<MemberBagViewModel>> Bag()
        {
            var member = await _unitOfWork.MemberService.GetMemberByUsernameWithBooksAndShipmentsAsync(User.GetUsername());
            if(member != null)
            {
                var confirmedShipments = member.Shipments?.Where(s => s.IsConfirmed == true)
                    .Select(b => new ShipmentBookViewModel { Book = b.Book, ShipDate = b.ShipDate.ToString("yyyy-MM-dd") })
                    .ToList();

                var unconfirmedShipments = member.Shipments?.Where(s => s.IsConfirmed == false && s.ShipDate != null)
                    .Select(b => new ShipmentBookViewModel { Book = b.Book, ShipDate = b.ShipDate.ToString("yyyy-MM-dd") })
                    .ToList();

                var unscheduledBooks = member.Books?.Where(b => member.Shipments.SingleOrDefault(s => s.BookId == b.Id) == null).ToList();

                var memberBagModel = new MemberBagViewModel
                {
                    Member = member,
                    ConfirmedShipments = confirmedShipments,
                    UnconfirmedShipments = unconfirmedShipments,
                    UnscheduledBooks = unscheduledBooks
                };

                return View(memberBagModel);
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

            var member = await _unitOfWork.MemberService.GetMemberWithMembershipTypeAsync((int)id);

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

                var member = await _unitOfWork.MemberService.GetMemberWithMembershipTypeAsync(updatedMember.Id);

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

            var member = await _unitOfWork.MemberService.GetMemberWithMembershipTypeAsync((int)id);

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
            var member = _unitOfWork.MemberService.Get(id);

            if (member == null)
            {
                return NotFound();
            }

            _unitOfWork.MemberService.Remove(member);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }
    }
}
