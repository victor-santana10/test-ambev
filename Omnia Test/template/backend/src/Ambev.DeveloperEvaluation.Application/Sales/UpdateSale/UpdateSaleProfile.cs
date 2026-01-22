using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Domain.Entities.Sales>();
            CreateMap<Domain.Entities.Sales, UpdateSaleResult>();
        }
    }
}
