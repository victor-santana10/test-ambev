using Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;

public class UpdateSaleItemProfile : Profile
{
    public UpdateSaleItemProfile()
    {
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
    }
}
