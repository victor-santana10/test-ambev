using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProducts
{
    public class UpdateProductsProfile : Profile
    {
        public UpdateProductsProfile()
        {
            CreateMap<UpdateProductsCommand, Domain.Entities.Products>();
            CreateMap<Domain.Entities.Products, UpdateProductsResult>();
        }
    }
}
