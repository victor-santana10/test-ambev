using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.DeleteSaleItem;

public class DeleteSaleItemHandler : IRequestHandler<DeleteSaleItemCommand, DeleteSaleItemResponse>
{
    private readonly ISalesItemsRepository _salesItemsRepository;

    public DeleteSaleItemHandler(
        ISalesItemsRepository salesItemsRepository)
    {
        _salesItemsRepository = salesItemsRepository;
    }

    public async Task<DeleteSaleItemResponse> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _salesItemsRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"SaleItems with ID {request.Id} not found");

        return new DeleteSaleItemResponse { Success = true };
    }
}
