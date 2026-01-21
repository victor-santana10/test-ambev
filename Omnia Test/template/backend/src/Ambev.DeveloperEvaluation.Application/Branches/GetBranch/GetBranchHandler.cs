using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Branches.GetBranch;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

public class GetBranchHandler : IRequestHandler<GetBranchCommand, GetBranchResult>
{
    private readonly IBranchesRepository _branchesRepository;
    private readonly IMapper _mapper;

    public GetBranchHandler(
        IBranchesRepository branchesRepository,
        IMapper mapper)
    {
        _branchesRepository = branchesRepository;
        _mapper = mapper;
    }

    public async Task<GetBranchResult> Handle(GetBranchCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetBranchValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await _branchesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"Branch with ID {request.Id} not found");

        return _mapper.Map<GetBranchResult>(user);
    }
}
