using Ambev.DeveloperEvaluation.Application.Products.CreateProducts;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.Branch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BranchesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateBranchRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateProductsCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<CreateBranchResponse>
                {
                    Success = true,
                    Message = "Branch created successfully",
                    Data = _mapper.Map<CreateBranchResponse>(response)
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
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateBranchResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBranch([FromQuery] Guid Id, [FromBody] UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Id = Id;
                var validator = new UpdateBranchRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<UpdateProductsCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<UpdateBranchResponse>
                {
                    Success = true,
                    Message = "Branch updated successfully",
                    Data = _mapper.Map<UpdateBranchResponse>(response)
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
        [ProducesResponseType(typeof(ApiResponseWithData<GetBranchResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBranch([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new GetBranchRequest { Id = id };
                var validator = new GetBranchRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetAllProductsCommand>(request.Id);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<GetBranchResponse>
                {
                    Success = true,
                    Message = "Branch retrieved successfully",
                    Data = _mapper.Map<GetBranchResponse>(response)
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
        public async Task<IActionResult> DeleteBranch([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new DeleteBranchRequest { Id = id };
                var validator = new DeleteBranchRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<DeleteProductsCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Branch deleted successfully"
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
