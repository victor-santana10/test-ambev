using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

public class CreateBranchProfile : Profile
{
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchCommand, Domain.Entities.Branches>();
        CreateMap<Domain.Entities.Branches, CreateBranchResult>();
    }
}
