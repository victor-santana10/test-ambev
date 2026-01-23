using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;
using Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<CreateBranchRequest, CreateBranchCommand>();
        CreateMap<CreateProductRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();

        CreateMap<UpdateBranchRequest, UpdateBranchCommand>();
        CreateMap<UpdateProductRequest, UpdateSaleItemCommand>();
        CreateMap<UpdateSaleRequest, UpdateSaleItemCommand>();
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
    }
}