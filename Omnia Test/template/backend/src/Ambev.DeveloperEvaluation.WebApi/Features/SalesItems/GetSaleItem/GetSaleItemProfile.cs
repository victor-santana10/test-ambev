using Ambev.DeveloperEvaluation.Application.SalesItems.GetAllSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItems.GetSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetAllSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetSaleItem;

public class GetAllSaleItemProfile : Profile
{
    public GetAllSaleItemProfile()
    {
        CreateMap<Guid, GetSaleItemCommand>().ConstructUsing(id => new GetSaleItemCommand(id));
        CreateMap<GetAllSaleItemResult, GetAllSaleItemResponse>();
    }
}
