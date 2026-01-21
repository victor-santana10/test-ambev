using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(branch => branch.Name).NotEmpty().Length(3, 50);
        RuleFor(branch => branch.IsActive).NotEmpty();
    }
}