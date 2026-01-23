using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Domain.Entities.Sales>()
            .ForMember(dest => dest.SalesItems, opt => opt.Ignore());
        CreateMap<Domain.Entities.Sales, CreateSaleResult>();
    }
}
