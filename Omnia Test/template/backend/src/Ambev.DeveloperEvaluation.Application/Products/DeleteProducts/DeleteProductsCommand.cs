using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProducts;

public record DeleteProductsCommand : IRequest<DeleteProductsResponse>
{
    public Guid Id { get; }

    public DeleteProductsCommand(Guid id)
    {
        Id = id;
    }
}
