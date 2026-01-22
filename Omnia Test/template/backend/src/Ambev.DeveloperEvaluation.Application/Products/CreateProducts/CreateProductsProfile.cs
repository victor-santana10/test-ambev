using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProducts;

public class CreateProductsProfile : Profile
{
    public CreateProductsProfile()
    {
        CreateMap<CreateProductsCommand, Domain.Entities.Products>();
        CreateMap<Domain.Entities.Products, CreateProductsResult>();
    }
}
