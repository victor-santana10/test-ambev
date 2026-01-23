using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;

public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
{
    private readonly ISalesItemsRepository _salesItemsRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    public CreateSaleItemHandler(ISalesItemsRepository salesItemsRepository, IDiscountService discountService, IMapper mapper)
    {
        _salesItemsRepository = salesItemsRepository;
        _discountService = discountService;
        _mapper = mapper;
    }

    public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var calc = _discountService.Calculate(command.Quantity, command.Price);
        command.Discount = calc.DiscountPercentage;
        command.Total = calc.Total;

        // TODO: Recalculate total sale value on insert

        var saleItem = _mapper.Map<Domain.Entities.SalesItems>(command);
        var createdSale = await _salesItemsRepository.CreateAsync(saleItem, cancellationToken);
        var result = _mapper.Map<CreateSaleItemResult>(createdSale);
        return result;
    }
}
