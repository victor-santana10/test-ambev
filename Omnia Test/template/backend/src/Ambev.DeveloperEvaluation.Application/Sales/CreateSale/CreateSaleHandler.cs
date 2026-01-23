using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IDiscountService _discountService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISalesRepository saleRepository, IMediator mediator, IMapper mapper, IDiscountService discountService)
    {
        _salesRepository = saleRepository;
        _mapper = mapper;
        _discountService = discountService;
        _mediator = mediator;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        command.GroupSalesItems();

        foreach (var item in command.SalesItems)
        {
            var calc = _discountService.Calculate(item.Quantity, item.Price);
            item.Discount = calc.DiscountPercentage;
            item.Total = calc.Total;
        }

        command.Total = command.SalesItems.Sum(i => i.Total);

        var sale = _mapper.Map<Domain.Entities.Sales>(command);
        var createdSale = await _salesRepository.CreateAsync(sale, cancellationToken);

        foreach (var item in command.SalesItems)
        {
            var createSaleItemCommand = new CreateSaleItemCommand
            {
                SalesId = createdSale.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount,
                Total = item.Total,
                IsActive = true
            };

            await _mediator.Send(createSaleItemCommand, cancellationToken);
        }

        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}
