using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProducts;

public class DeleteProductsHandler : IRequestHandler<DeleteProductsCommand, DeleteProductsResponse>
{
    private readonly IProductsRepository _productsRepository;

    public DeleteProductsHandler(
        IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<DeleteProductsResponse> Handle(DeleteProductsCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _productsRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Products with ID {request.Id} not found");

        return new DeleteProductsResponse { Success = true };
    }
}
