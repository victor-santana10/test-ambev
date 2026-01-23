using Ambev.DeveloperEvaluation.Application.SalesItems.GetAllSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetAllSaleItem;

public class GetAllSaleItemProfile : Profile
{
    public GetAllSaleItemProfile()
    {
        CreateMap<Guid, GetAllSaleItemCommand>().ConstructUsing(id => new GetAllSaleItemCommand(id));
        CreateMap<GetSaleItemAllResult, GetSaleItemAllResponse>();
        CreateMap<GetAllSaleItemResult, GetAllSaleItemResponse>();
    }
}
