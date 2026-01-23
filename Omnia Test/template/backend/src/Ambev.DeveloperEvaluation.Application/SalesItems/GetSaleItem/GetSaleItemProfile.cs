using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.GetSaleItem;

public class GetSaleItemProfile : Profile
{
    public GetSaleItemProfile()
    {
        CreateMap<Domain.Entities.SalesItems, GetSaleItemResult>();
    }
}
