using BookSmart.Dtos;
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
        private readonly IShipmentService _shipmentService;

        public ShipmentApiController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IShipmentService shipmentService)
        {
            _unitOfWork = unitOfWork;
            _shipmentService = shipmentService;
            _httpContextAccessor = httpContextAccessor;

            _loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveShipment")]
        [Authorize]
        public async Task<ActionResult> SaveShipment(ShipmentFormDto shipmentDto)
        {
            RequestResponse<int> requestResponse = new RequestResponse<int>();

            try
            {
                requestResponse.Status = await _shipmentService.AddUpdateAsync(shipmentDto);

                if (requestResponse.Status == 1)
                {
                    requestResponse.Message = Utility.ResponseHelper.ShipmentUpdated;
                }
                if (requestResponse.Status == 2)
                {
                    requestResponse.Message = Utility.ResponseHelper.ShipmentAdded;
                }

                if (_unitOfWork.HasChanges())
                {
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                requestResponse.Message = ex.Message;
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
            }
            return Ok(requestResponse);
        }

        [HttpGet]
        [Route("GetShipmentDataByMember")]
        public async Task<ActionResult> GetShipmentDataByMember(int memberId)
        {
            RequestResponse<List<ShipmentFormDto>> requestResponse = new RequestResponse<List<ShipmentFormDto>>();

            try
            {
                requestResponse.Data = await _shipmentService.ShipmentsByMemberId(memberId);
                requestResponse.Status = Utility.ResponseHelper.SuccessCode;
            }
            catch (Exception ex)
            {
                requestResponse.Message = ex.Message;
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
            }
            return Ok(requestResponse);
        }

        [HttpGet]
        [Route("GetShipmentById/{id}")]
        public async Task<ActionResult> GetShipmentById(int id)
        {
            RequestResponse<ShipmentFormDto> requestResponse = new RequestResponse<ShipmentFormDto>();

            try
            {
                requestResponse.Data = await _shipmentService.ShipmentById(id);
                requestResponse.Status = Utility.ResponseHelper.SuccessCode;
            }
            catch (Exception ex)
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
                requestResponse.Status = await _shipmentService.DeleteShipment(id);
                requestResponse.Message = requestResponse.Status == 1
                    ? Utility.ResponseHelper.ShipmentDeleted
                    : Utility.ResponseHelper.ShipmentDeleteError;

                if (_unitOfWork.HasChanges())
                {
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
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
                var result = await _shipmentService.ConfirmShipment(id);
                if (result > 0)
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
            catch (Exception ex)
            {
                requestResponse.Status = Utility.ResponseHelper.FailureCode;
                requestResponse.Message = ex.Message;
            }
            return Ok(requestResponse);
        }
    }
}
