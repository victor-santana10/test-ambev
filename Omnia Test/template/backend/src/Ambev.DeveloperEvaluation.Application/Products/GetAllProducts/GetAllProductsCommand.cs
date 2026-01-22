using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public record GetAllProductsCommand : IRequest<GetAllProductsResult>
{
    public Guid Id { get; }

    public GetAllProductsCommand(Guid id)
    {
        Id = id;
    }
}
