using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem
{
    public class UpdateSaleItemHandler : IRequestHandler<UpdateSaleItemCommand, UpdateSaleItemResult>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;
        private readonly IMapper _mapper;

        public UpdateSaleItemHandler(ISalesItemsRepository salesItemsRepository, IMapper mapper)
        {
            _salesItemsRepository = salesItemsRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleItemResult> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleItemCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var saleItem = _mapper.Map<Domain.Entities.SalesItems>(request);
            var updatedUser = await _salesItemsRepository.UpdateAsync(saleItem, cancellationToken);
            var result = _mapper.Map<UpdateSaleItemResult>(updatedUser);
            return result;
        }
    }
}
