using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProducts
{
    public class UpdateProductsHandler : IRequestHandler<UpdateProductsCommand, UpdateProductsResult>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public UpdateProductsHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProductsResult> Handle(UpdateProductsCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductsCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var product = _mapper.Map<Domain.Entities.Products>(request);
            var updatedUser = await _productsRepository.UpdateAsync(product, cancellationToken);
            var result = _mapper.Map<UpdateProductsResult>(updatedUser);
            return result;
        }
    }
}
