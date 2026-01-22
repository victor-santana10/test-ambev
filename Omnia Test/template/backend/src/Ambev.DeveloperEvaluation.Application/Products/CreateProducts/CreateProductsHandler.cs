using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProducts;

public class CreateProductsHandler : IRequestHandler<CreateProductsCommand, CreateProductsResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public CreateProductsHandler(IProductsRepository branchRepository, IMapper mapper)
    {
        _productsRepository = branchRepository;
        _mapper = mapper;
    }

    public async Task<CreateProductsResult> Handle(CreateProductsCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductsCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProducts = await _productsRepository.GetByNameAsync(command.Name, cancellationToken);
        if (existingProducts != null)
            throw new InvalidOperationException($"Products with name {command.Name} already exists");

        var branch = _mapper.Map<Domain.Entities.Products>(command);

        var createdProducts = await _productsRepository.CreateAsync(branch, cancellationToken);
        var result = _mapper.Map<CreateProductsResult>(createdProducts);
        return result;
    }
}
