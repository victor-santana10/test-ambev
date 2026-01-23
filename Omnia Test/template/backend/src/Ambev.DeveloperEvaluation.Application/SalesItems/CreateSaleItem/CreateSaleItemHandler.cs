using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;

public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
{
    private readonly ISalesItemsRepository _salesItemsRepository;
    private readonly IMapper _mapper;

    public CreateSaleItemHandler(ISalesItemsRepository salesItemsRepository, IMapper mapper)
    {
        _salesItemsRepository = salesItemsRepository;
        _mapper = mapper;
    }

    public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var saleItem = _mapper.Map<Domain.Entities.SalesItems>(command);
        var createdSale = await _salesItemsRepository.CreateAsync(saleItem, cancellationToken);
        var result = _mapper.Map<CreateSaleItemResult>(createdSale);
        return result;
    }
}
