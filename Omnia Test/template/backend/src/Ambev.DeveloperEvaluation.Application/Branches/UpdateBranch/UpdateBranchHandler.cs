using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch
{
    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResult>
    {
        private readonly IBranchesRepository _branchesRepository;
        private readonly IMapper _mapper;
        public UpdateBranchHandler(IBranchesRepository branchesRepository, IMapper mapper)
        {
            _branchesRepository = branchesRepository;
            _mapper = mapper;
        }

        public async Task<UpdateBranchResult> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBranchCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var branch = _mapper.Map<Domain.Entities.Branches>(request);
            var updatedUser = await _branchesRepository.UpdateAsync(branch, cancellationToken);
            var result = _mapper.Map<UpdateBranchResult>(updatedUser);
            return result;
        }
    }
}
