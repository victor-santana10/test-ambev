using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.GetAllSaleItem;

public class GetAllSaleItemProfile : Profile
{
    public GetAllSaleItemProfile()
    {
        CreateMap<Domain.Entities.SalesItems, GetSaleItemAllResult>();
    }
}
