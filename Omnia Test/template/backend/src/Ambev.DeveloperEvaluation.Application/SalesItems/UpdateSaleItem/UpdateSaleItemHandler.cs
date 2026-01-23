using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem
{
    public class UpdateSaleItemHandler : IRequestHandler<UpdateSaleItemCommand, UpdateSaleItemResult>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public UpdateSaleItemHandler(ISalesItemsRepository salesItemsRepository, IDiscountService discountService, IMapper mapper)
        {
            _salesItemsRepository = salesItemsRepository;
            _discountService = discountService;
            _mapper = mapper;
        }

        public async Task<UpdateSaleItemResult> Handle(UpdateSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleItemCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var calc = _discountService.Calculate(command.Quantity, command.Price);
            command.Discount = calc.DiscountPercentage;
            command.Total = calc.Total;

            var saleItem = _mapper.Map<Domain.Entities.SalesItems>(command);
            var updatedUser = await _salesItemsRepository.UpdateAsync(saleItem, cancellationToken);
            var result = _mapper.Map<UpdateSaleItemResult>(updatedUser);
            return result;
        }
    }
}
