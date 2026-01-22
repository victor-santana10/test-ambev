using Ambev.DeveloperEvaluation.Application.Products.UpdateProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;

public class UpdateBranchProfile : Profile
{
    public UpdateBranchProfile()
    {
        CreateMap<UpdateBranchRequest, UpdateProductsCommand>();
        CreateMap<UpdateProductsResult, UpdateBranchResponse>();
    }
}
