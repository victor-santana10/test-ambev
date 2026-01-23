using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;

public class UpdateBranchRequestValidator : AbstractValidator<UpdateBranchRequest>
{
    public UpdateBranchRequestValidator()
    {
        RuleFor(branch => branch.Id).NotEmpty().WithMessage("Branch ID is required");
        RuleFor(branch => branch.Name).NotEmpty().Length(3, 50);
    }
}
