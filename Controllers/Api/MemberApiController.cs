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

        private readonly IMemberService _memberService;

        public MemberApiController(IUnitOfWork unitOfWork, IMemberService memberService)
        {
            _unitOfWork = unitOfWork;
            _memberService = memberService;
        }

        [HttpPost]
        [Route("AddToBag")]
        [Authorize]
        public async Task<ActionResult> AddToBag(int bookId)
        {
            RequestResponse<int> response = new RequestResponse<int>();
            try
            {
                var member = await _unitOfWork.Members.GetMemberByUsernameWithBooksAsync(User.GetUsername());
                var result = _memberService.AddToBag(member, bookId);

                if (result == 2)
                {
                    response.Message = Utility.ResponseHelper.BookAlreadyInBag;
                    response.Status = Utility.ResponseHelper.FailureCode;
                }

                else
                {
                    response.Message = Utility.ResponseHelper.BookAddedToBag;
                    response.Status = Utility.ResponseHelper.SuccessCode;
                }

                if (_unitOfWork.HasChanges())
                {
                    await _unitOfWork.CompleteAsync();
                }

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
                var member = await _memberService.GetMemberByUsernameWithBooksAndShipmentsAsync(User.GetUsername());
                var result = _memberService.RemoveFromBag(member, bookId);
                if (result == 1)
                {
                    response.Message = Utility.ResponseHelper.BookRemovedFromBag;
                    response.Status = Utility.ResponseHelper.SuccessCode;
                }

                else
                {
                    response.Message = Utility.ResponseHelper.BookIsNotInBag;
                    response.Status = Utility.ResponseHelper.FailureCode;
                }

                if (_unitOfWork.HasChanges())
                {
                    await _unitOfWork.CompleteAsync();
                }

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
