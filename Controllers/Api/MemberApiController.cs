using BookSmart.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookSmart.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookSmart.ViewModels;
using System;

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
                var member = await _unitOfWork.Members.GetMemberByUsernameWithBooksAsync(User.GetUsername());
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

        [Route("RemoveFromBag")]
        [Authorize]
        public async Task<ActionResult> RemoveFromBag(int bookId)
        {
            RequestResponse<int> response = new RequestResponse<int>();
            try
            {
                var member = await _unitOfWork.Members.GetMemberByUsernameWithBooksAsync(User.GetUsername());
                var bookToRemove = _unitOfWork.BookService.Get(bookId);
                if (member.Books.Contains(bookToRemove))
                {
                    member.Books.Remove(bookToRemove);
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
