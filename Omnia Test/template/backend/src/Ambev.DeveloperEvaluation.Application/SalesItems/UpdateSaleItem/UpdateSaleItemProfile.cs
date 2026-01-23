using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem
{
    public class UpdateSaleItemProfile : Profile
    {
        public UpdateSaleItemProfile()
        {
            CreateMap<UpdateSaleItemCommand, Domain.Entities.SalesItems>();
            CreateMap<Domain.Entities.SalesItems, UpdateSaleItemResult>();
        }
    }
}
