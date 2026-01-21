using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Branches.GetBranch;

public class GetBranchValidator : AbstractValidator<GetBranchCommand>
{
    public GetBranchValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
}
