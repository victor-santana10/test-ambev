using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

public record GetBranchCommand : IRequest<GetBranchResult>
{
    public Guid Id { get; }

    public GetBranchCommand(Guid id)
    {
        Id = id;
    }
}
