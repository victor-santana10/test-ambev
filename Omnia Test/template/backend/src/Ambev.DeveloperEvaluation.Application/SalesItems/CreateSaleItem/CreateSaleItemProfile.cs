using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;

public class CreateSaleItemProfile : Profile
{
    public CreateSaleItemProfile()
    {
        CreateMap<CreateSaleItemCommand, Domain.Entities.SalesItems>();
        CreateMap<Domain.Entities.SalesItems, CreateSaleItemResult>();
    }
}
