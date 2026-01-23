using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.GetAllSaleItem;

public record GetAllSaleItemCommand : IRequest<GetAllSaleItemResult>
{
    public Guid Id { get; }

    public GetAllSaleItemCommand(Guid id)
    {
        Id = id;
    }
}
