using Ambev.DeveloperEvaluation.Application.Products.UpdateProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductsCommand>();
        CreateMap<UpdateProductsResult, UpdateProductResponse>();
    }
}
