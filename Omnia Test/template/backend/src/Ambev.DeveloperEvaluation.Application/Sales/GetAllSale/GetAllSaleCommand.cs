using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

public record GetAllSaleCommand : IRequest<GetAllSaleResult>
{
    public Guid Id { get; }

    public GetAllSaleCommand(Guid id)
    {
        Id = id;
    }
}
