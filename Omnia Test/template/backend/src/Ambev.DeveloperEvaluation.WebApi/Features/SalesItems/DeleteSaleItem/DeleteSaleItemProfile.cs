using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.DeleteSaleItem;

public class DeleteSaleItemProfile : Profile
{
    public DeleteSaleItemProfile()
    {
        CreateMap<Guid, Application.SalesItems.DeleteSaleItem.DeleteSaleItemCommand>()
            .ConstructUsing(id => new Application.SalesItems.DeleteSaleItem.DeleteSaleItemCommand(id));
    }
}
