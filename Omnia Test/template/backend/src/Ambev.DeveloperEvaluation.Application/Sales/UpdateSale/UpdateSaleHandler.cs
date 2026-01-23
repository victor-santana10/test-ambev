using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISalesRepository _productsRepository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISalesRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // TODO: Refactor to alter SalesItems if necessary and valid FKs

            var product = _mapper.Map<Domain.Entities.Sales>(request);
            var updatedUser = await _productsRepository.UpdateAsync(product, cancellationToken);
            var result = _mapper.Map<UpdateSaleResult>(updatedUser);
            return result;
        }
    }
}
