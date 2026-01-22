using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, GetAllProductsResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(
        IProductsRepository productsRepository,
        IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<GetAllProductsResult> Handle(
        GetAllProductsCommand request,
        CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetAllAsync();

        if (!products.Any())
            return new GetAllProductsResult();

        var p = products.ToList();

        var result = _mapper.Map<List<GetProductsAllResult>>(p);
        return new GetAllProductsResult { Products = result };
    }
}
