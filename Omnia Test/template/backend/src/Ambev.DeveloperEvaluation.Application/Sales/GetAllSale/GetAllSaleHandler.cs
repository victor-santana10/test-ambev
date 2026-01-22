using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

public class GetAllSaleHandler : IRequestHandler<GetAllSaleCommand, GetAllSaleResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    public GetAllSaleHandler(
        ISalesRepository salesRepository,
        IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

    public async Task<GetAllSaleResult> Handle(
        GetAllSaleCommand request,
        CancellationToken cancellationToken)
    {
        var sales = await _salesRepository.GetAllAsync();

        if (!sales.Any())
            return new GetAllSaleResult();

        var p = sales.ToList();

        var result = _mapper.Map<List<GetSaleAllResult>>(p);
        return new GetAllSaleResult { Sales = result };
    }
}
