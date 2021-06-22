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

        private readonly IMemberService _memberService;

        public ShipmentController(IUnitOfWork unitOfWork, IMemberService memberService)
        {
            _unitOfWork = unitOfWork;
            _memberService = memberService;
        }

        [HttpGet("Calendar/{id?}")]
        public async Task<ActionResult<ShipmentViewModel>> Calendar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var viewModel = await _memberService.GetMemberShipmentsModel((int)id);

            return View(viewModel);
        }
    }
}
