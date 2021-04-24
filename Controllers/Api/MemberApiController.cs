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
                var result = await _unitOfWork.MemberService.AddToBagAsync(member, bookId);

                if (result == 2)
                {
                    response.Message = Utility.ResponseHelper.BookAlreadyInBag;
                    response.Status = Utility.ResponseHelper.FailureCode;
                    return Ok(response);
                }

                response.Message = Utility.ResponseHelper.BookAddedToBag;
                response.Status = Utility.ResponseHelper.SuccessCode;

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
                var result = await _unitOfWork.MemberService.RemoveFromBagAsync(member, bookId);
                if (result == 1)
                {
                    response.Message = Utility.ResponseHelper.BookRemovedFromBag;
                    response.Status = Utility.ResponseHelper.SuccessCode;

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
