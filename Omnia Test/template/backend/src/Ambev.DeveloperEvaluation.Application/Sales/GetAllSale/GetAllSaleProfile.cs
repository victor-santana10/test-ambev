using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

public class GetAllSaleProfile : Profile
{
    public GetAllSaleProfile()
    {
        CreateMap<Domain.Entities.Sales, GetSaleAllResult>();
    }
}
