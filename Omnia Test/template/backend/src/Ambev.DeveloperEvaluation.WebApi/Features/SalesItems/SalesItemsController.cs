using Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItems.DeleteSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItems.GetAllSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.DeleteSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetAllSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems
{

    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleItemResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSaleItem([FromBody] CreateSaleItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateSaleItemRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateSaleItemCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<CreateSaleItemResponse>
                {
                    Success = true,
                    Message = "SaleItem created successfully",
                    Data = _mapper.Map<CreateSaleItemResponse>(response)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSaleItem([FromQuery] Guid Id, [FromBody] UpdateSaleItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Id = Id;
                var validator = new UpdateSaleItemRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<UpdateSaleItemCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<UpdateSaleItemResponse>
                {
                    Success = true,
                    Message = "SaleItem updated successfully",
                    Data = _mapper.Map<UpdateSaleItemResponse>(response)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleItemResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleItem([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new GetSaleItemRequest { Id = id };
                var validator = new GetSaleItemRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetSaleItemCommand>(request.Id);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<GetSaleItemResult>
                {
                    Success = true,
                    Message = "SaleItem retrieved successfully",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("BySaleId/{SaleId}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetAllSaleItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSaleItemBySaleId(Guid SaleId)
        {
            try
            {
                var command = _mapper.Map<GetAllSaleItemCommand>(SaleId);
                var response = await _mediator.Send(command, new CancellationToken());

                return Ok(new ApiResponseWithData<GetAllSaleItemResult>
                {
                    Success = true,
                    Message = "SaleItems retrieved successfully",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSaleItem([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new DeleteSaleItemRequest { Id = id };
                var validator = new DeleteSaleItemRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<DeleteSaleItemCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "SaleItem deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
