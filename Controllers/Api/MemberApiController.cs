using BookSmart.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookSmart.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookSmart.ViewModels;
using System;
using System.Linq;

namespace BookSmart.Controllers.Api
{
    [Route("api/Member")]
    [ApiController]
    public class MemberApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("AddToBag")]
        [Authorize]
        public async Task<ActionResult> AddToBag(int bookId)
        {
            RequestResponse<int> response = new RequestResponse<int>();
            try
            {
                var member = await _unitOfWork.MemberService.GetMemberByUsernameWithBooksAsync(User.GetUsername());
                var bookToAdd = _unitOfWork.BookService.Get(bookId);
                if (member.Books.Contains(bookToAdd))
                {
                    response.Message = Utility.ResponseHelper.BookAlreadyInBag;
                    response.Status = Utility.ResponseHelper.FailureCode;
                    return Ok(response);
                }
                member.Books.Add(bookToAdd);
                response.Message = Utility.ResponseHelper.BookAddedToBag;
                response.Status = Utility.ResponseHelper.SuccessCode;

                await _unitOfWork.CompleteAsync();

                return Ok(response);
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = Utility.ResponseHelper.FailureCode;
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("RemoveFromBag")]
        [Authorize]
        public async Task<ActionResult> RemoveFromBag(int bookId)
        {
            RequestResponse<int> response = new RequestResponse<int>();
            try
            {
                var member = await _unitOfWork.MemberService.GetMemberByUsernameWithBooksAndShipmentsAsync(User.GetUsername());
                var bookToRemove = _unitOfWork.BookService.Get(bookId);
                if (member.Books.Contains(bookToRemove))
                {
                    member.Books.Remove(bookToRemove);

                    var shipment = member.Shipments.FirstOrDefault(s => s.BookId == bookToRemove.Id);
                    if(shipment != null)
                    {
                        member.Shipments.Remove(shipment);
                        await _unitOfWork.ShipmentService.DeleteShipment(shipment.Id);
                    }

                    response.Message = Utility.ResponseHelper.BookRemovedFromBag;
                    response.Status = Utility.ResponseHelper.SuccessCode;

                    await _unitOfWork.CompleteAsync();

                    return Ok(response);
                }
                response.Message = Utility.ResponseHelper.BookIsNotInBag;
                response.Status = Utility.ResponseHelper.FailureCode;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = Utility.ResponseHelper.FailureCode;
                return Ok(response);
            }
        }
    }
}
