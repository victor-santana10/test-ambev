using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;

public class CreateBranchProfile : Profile
{
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchRequest, CreateProductsCommand>();
        CreateMap<CreateProductsResult, CreateBranchResponse>();
    }
}
