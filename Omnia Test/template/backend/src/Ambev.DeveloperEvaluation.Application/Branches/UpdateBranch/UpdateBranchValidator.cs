using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch
{
    internal class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchCommandValidator()
        {
            RuleFor(branch => branch.Id).NotEmpty().NotNull();
            RuleFor(branch => branch.IsActive).NotEmpty();
            RuleFor(branch => branch.Name).NotEmpty().Length(3, 50);
        }
    }
}
