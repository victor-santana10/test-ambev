using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProducts;

public class DeleteProductProfile : Profile
{
    public DeleteProductProfile()
    {
        CreateMap<Guid, Application.Products.DeleteProducts.DeleteProductsCommand>()
            .ConstructUsing(id => new Application.Products.DeleteProducts.DeleteProductsCommand(id));
    }
}
