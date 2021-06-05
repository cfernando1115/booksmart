using BookSmart.Interfaces;
using BookSmart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookSmart.Controllers.Api
{
    [Route("api/Shipment")]
    [ApiController]
    public class ShipmentApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _loginUserId;
        private readonly string _role;

        public ShipmentApiController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;

            _loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveShipment")]
        [Authorize]
        public async Task<ActionResult> SaveShipment(ShipmentFormViewModel shipmentModel)
        {
            RequestResponse<int> requestResponse = new RequestResponse<int>();

            try
            {
                requestResponse.Status = await _unitOfWork.ShipmentService.AddUpdateAsync(shipmentModel);

                if(requestResponse.Status == 1)
                {
                    requestResponse.Message = Utility.ResponseHelper.ShipmentUpdated;
                }
                if(requestResponse.Status == 2)
                {
                    requestResponse.Message = Utility.ResponseHelper.ShipmentAdded;
                }

                if (_unitOfWork.HasChanges())
                {
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch(Exception ex)
            {
                requestResponse.Message = ex.Message;
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
            }
            return Ok(requestResponse);
        }

        [HttpGet]
        [Route("GetShipmentDataByMember")]
        public ActionResult GetShipmentDataByMember(int memberId)
        {
            RequestResponse<List<ShipmentFormViewModel>> requestResponse = new RequestResponse<List<ShipmentFormViewModel>>();

            try
            {
                requestResponse.Data = _unitOfWork.ShipmentService.ShipmentsByMemberId(memberId);
                requestResponse.Status = Utility.ResponseHelper.SuccessCode;
            }
            catch(Exception ex)
            {
                requestResponse.Message = ex.Message;
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
            }
            return Ok(requestResponse);
        }

        [HttpGet]
        [Route("GetShipmentById/{id}")]
        public ActionResult GetShipmentById(int id)
        {
            RequestResponse<ShipmentFormViewModel> requestResponse = new RequestResponse<ShipmentFormViewModel>();

            try
            {
                requestResponse.Data = _unitOfWork.ShipmentService.ShipmentById(id);
                requestResponse.Status = Utility.ResponseHelper.SuccessCode;
            }
            catch(Exception ex)
            {
                requestResponse.Message = ex.Message;
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
            }
            return Ok(requestResponse);
        }

        [HttpGet]
        [Route("DeleteShipment/{id}")]
        public async Task<ActionResult> DeleteShipment(int id)
        {
            RequestResponse<int> requestResponse = new RequestResponse<int>();
            try
            {
                requestResponse.Status = await _unitOfWork.ShipmentService.DeleteShipment(id);
                requestResponse.Message = requestResponse.Status == 1
                    ? Utility.ResponseHelper.ShipmentDeleted
                    : Utility.ResponseHelper.ShipmentDeleteError;

                if(_unitOfWork.HasChanges())
                {
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch(Exception ex)
            {
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
                requestResponse.Message = ex.Message;
            }
            return Ok(requestResponse);
        }

        [HttpGet]
        [Route("ConfirmShipment/{id}")]
        public async Task<ActionResult> ConfirmShipment(int id)
        {
            RequestResponse<int> requestResponse = new RequestResponse<int>();
            try
            {
                var result = await _unitOfWork.ShipmentService.ConfirmShipment(id);
                if(result > 0)
                {
                    requestResponse.Status = result;
                    requestResponse.Message = Utility.ResponseHelper.ShipmentConfirmed;

                    if (_unitOfWork.HasChanges())
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                }
                else
                {
                    requestResponse.Message = result == -1 
                        ? Utility.ResponseHelper.ShipmentOverBooksRemainingError 
                        : Utility.ResponseHelper.ShipmentConfirmError;

                    requestResponse.Status = Utility.ResponseHelper.FailureCode;
                }
            }
            catch(Exception ex)
            {
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
                requestResponse.Message = ex.Message;
            }
            return Ok(requestResponse);
        }
    }
}
