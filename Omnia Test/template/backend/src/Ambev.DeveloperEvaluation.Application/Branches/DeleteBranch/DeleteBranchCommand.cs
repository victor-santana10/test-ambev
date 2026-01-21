using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;

public record DeleteBranchCommand : IRequest<DeleteBranchResponse>
{
    public Guid Id { get; }

    public DeleteBranchCommand(Guid id)
    {
        Id = id;
    }
}
