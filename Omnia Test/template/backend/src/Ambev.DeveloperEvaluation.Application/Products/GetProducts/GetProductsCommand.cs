using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public record GetProductsCommand : IRequest<GetProductsResult>
{
    public Guid Id { get; }

    public GetProductsCommand(Guid id)
    {
        Id = id;
    }
}
