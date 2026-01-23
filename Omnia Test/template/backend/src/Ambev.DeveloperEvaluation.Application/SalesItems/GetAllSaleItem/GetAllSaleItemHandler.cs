using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.GetAllSaleItem;

public class GetAllSaleItemHandler : IRequestHandler<GetAllSaleItemCommand, GetAllSaleItemResult>
{
    private readonly ISalesItemsRepository _salesItemsRepository;
    private readonly IMapper _mapper;

    public GetAllSaleItemHandler(
        ISalesItemsRepository salesItemsRepository,
        IMapper mapper)
    {
        _salesItemsRepository = salesItemsRepository;
        _mapper = mapper;
    }

    public async Task<GetAllSaleItemResult> Handle(
        GetAllSaleItemCommand request,
        CancellationToken cancellationToken)
    {
        var sales = await _salesItemsRepository.GetAllBySalesIdAsync(request.Id);

        if (!sales.Any())
            return new GetAllSaleItemResult();

        var p = sales.ToList();

        var result = _mapper.Map<List<GetSaleItemAllResult>>(p);
        return new GetAllSaleItemResult { SalesItems = result };
    }
}
