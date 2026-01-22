using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsProfile : Profile
{
    public GetAllProductsProfile()
    {
        CreateMap<Domain.Entities.Products, GetProductsAllResult>();
    }
}
