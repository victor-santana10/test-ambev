using Ambev.DeveloperEvaluation.Application.Products.CreateProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductsCommand>();
        CreateMap<CreateProductsResult, CreateProductResponse>();
    }
}
