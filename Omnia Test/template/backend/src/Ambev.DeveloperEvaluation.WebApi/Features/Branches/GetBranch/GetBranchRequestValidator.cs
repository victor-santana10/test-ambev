using Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;

public class GetBranchRequestValidator : AbstractValidator<UpdateBranchRequest>
{
    public GetBranchRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
}
