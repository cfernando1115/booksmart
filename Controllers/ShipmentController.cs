using BookSmart.Interfaces;
using BookSmart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookSmart.Controllers
{
    [Authorize]
    [Route("Shipment")]
    public class ShipmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShipmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Calendar/{id?}")]
        public async Task<ActionResult<ShipmentViewModel>> Calendar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var member = await _unitOfWork.MemberService.GetMemberByIdWithBooksAsync((int)id);

            ShipmentViewModel viewModel = new ShipmentViewModel
            {
                Member = member,
                Books = member.Books
            };
            return View(viewModel);
        }
    }
}
