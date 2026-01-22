using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProduct;

public class GetAllProductProfile : Profile
{
    public GetAllProductProfile()
    {
        CreateMap<Guid, GetAllProductsCommand>().ConstructUsing(id => new GetAllProductsCommand(id));
        CreateMap<GetProductsAllResult, GetProductAllResponse>();
        CreateMap<GetAllProductsResult, GetAllProductResponse>();
    }
}
