using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.Branch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;

public class GetBranchProfile : Profile
{
    public GetBranchProfile()
    {
        CreateMap<Guid, GetBranchCommand>().ConstructUsing(id => new GetBranchCommand(id));
        CreateMap<GetBranchResult, GetBranchResponse>();
    }
}
