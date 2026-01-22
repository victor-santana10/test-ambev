using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsCommand, GetProductsResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetProductsHandler(
        IProductsRepository productsRepository,
        IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<GetProductsResult> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var products = await _productsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (products == null)
            throw new KeyNotFoundException($"Products with ID {request.Id} not found");

        return _mapper.Map<GetProductsResult>(products);
    }
}
