using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.GetSaleItem;

public class GetSaleItemHandler : IRequestHandler<GetSaleItemCommand, GetSaleItemResult>
{
    private readonly ISalesItemsRepository _salesItemsRepository;
    private readonly IMapper _mapper;

    public GetSaleItemHandler(
        ISalesItemsRepository salesItemsRepository,
        IMapper mapper)
    {
        _salesItemsRepository = salesItemsRepository;
        _mapper = mapper;
    }

    public async Task<GetSaleItemResult> Handle(GetSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sales = await _salesItemsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sales == null)
            throw new KeyNotFoundException($"SaleItems with ID {request.Id} not found");

        return _mapper.Map<GetSaleItemResult>(sales);
    }
}
