using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch
{
    public class UpdateBranchProfile : Profile
    {
        public UpdateBranchProfile()
        {
            CreateMap<UpdateBranchCommand, Domain.Entities.Branches>();
            CreateMap<Domain.Entities.Branches, UpdateBranchResult>();
        }
    }
}
