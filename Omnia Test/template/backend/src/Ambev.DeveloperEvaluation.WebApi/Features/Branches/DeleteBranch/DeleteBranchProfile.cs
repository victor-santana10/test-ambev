using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;

public class DeleteBranchProfile : Profile
{
    public DeleteBranchProfile()
    {
        CreateMap<Guid, Application.Branches.DeleteBranch.DeleteBranchCommand>()
            .ConstructUsing(id => new Application.Branches.DeleteBranch.DeleteBranchCommand(id));
    }
}
