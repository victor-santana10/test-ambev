using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetAllSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Net.Http.Json;
using System.Xml.Linq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Tests;

public class SalesItemsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SalesItemsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // TODO: Create more tests to cover all scenarios

    private async Task<Guid> GetBranch(string name)
    {
        var requestBranches = new
        {
            name = name,
            isActive = true
        };

        var response = await _client.PostAsJsonAsync("/api/Branches", requestBranches);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var resultBranch = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        var result = resultBranch.Data.Id;
        return result;
    }

    private async Task<Guid> GetUser(string name, string email)
    {
        var requestUsers = new
        {
            username = name,
            password = "P@ssword123",
            phone = "11912345678",
            email = email,
            status = 1,
            role = 1
        };

        var response = await _client.PostAsJsonAsync("/api/Users", requestUsers);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var resultUsers = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
        var result = resultUsers.Data.Id;
        return result;
    }

    private async Task<Guid> GetProduct()
    {
        var request = new
        {
            name = $"Test Product {Guid.NewGuid()}",
            Price = 200,
            isActive = true
        };

        var response = await _client.PostAsJsonAsync("/api/Products", request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var resultProduct = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        return resultProduct.Data.Id;
    }

    private async Task<ApiResponseWithData<CreateSaleResponse>> CreateSales()
    {
        var request = new
        {
            userId = await GetUser("User " + Guid.NewGuid().ToString(), Guid.NewGuid().ToString() + "@test.com"),
            branchId = await GetBranch("Branch " + Guid.NewGuid().ToString()),
            isActive = true,
            salesItems = new[]
            {
                new
                {
                    productId = await GetProduct(),
                    quantity = 4,
                    price = 200,
                    isActive = true
                }
            }
        };

        var response = await _client.PostAsJsonAsync("/api/Sales", request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
        return result;
    }

    private async Task<ApiResponseWithData<CreateSaleItemResponse>> CreateSalesItem(Guid salesId)
    {
        var request = new
        {
            SalesId = salesId,
            ProductId = await GetProduct(),
            Quantity = 1,
            Price = 200,
            IsActive = true
        };
        var response = await _client.PostAsJsonAsync("/api/SalesItems", request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleItemResponse>>();
        return result;
    }

    [Fact]
    public async Task POST_api_Sales_Items_Should_Create_Sale_Item()
    {
        var resultSale = await CreateSales();
        await CreateSalesItem(resultSale.Data.Id);
    }

    [Fact]
    public async Task PUT_api_Sales_Items_Should_Update_Sale_Item()
    {
        var resultSale = await CreateSales();
        var saleItem = await CreateSalesItem(resultSale.Data.Id);

        var id = saleItem.Data.Id;
        var updateRequest = new
        {
            Id = id,
            SalesId = resultSale.Data.Id,
            ProductId = await GetProduct(),
            Quantity = 4,
            Price = 400,
            IsActive = true
        };

        var response = await _client.PutAsJsonAsync($"/api/SalesItems?id={id}", updateRequest);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateSaleResponse>>();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_api_Sales_Items_Id_Should_Return_Sale_Item()
    {
        var resultSale = await CreateSales();
        var result = await CreateSalesItem(resultSale.Data.Id);

        var id = result.Data.Id;
        var response = await _client.GetAsync($"/api/SalesItems/{id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var saleItem = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetSaleItemResponse>>();

        Assert.NotNull(saleItem);
        Assert.Equal(id, saleItem.Data.Id);
        Assert.Equal(200, saleItem.Data.Total);
        Assert.True(saleItem.Data.IsActive);
    }

    [Fact]
    public async Task GET_All_api_Sales_Items_Should_Return_List_Sale_Item()
    {
        var resultSale = await CreateSales();
        var result = await CreateSalesItem(resultSale.Data.Id);

        var id = result.Data.Id;
        var response = await _client.GetAsync($"/api/SalesItems/BySaleId/{resultSale.Data.Id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var salesItems = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetAllSaleItemResponse>>();
        Assert.NotNull(salesItems);
        Assert.True(salesItems.Data.SalesItems.Exists(x => x.Id.Equals(id)));
    }

    [Fact]
    public async Task DELETE_api_Sales_Items_Id_Should_Delete_Sale_Item()
    {
        var sale = await CreateSales();
        var saleItem = await CreateSalesItem(sale.Data.Id);

        var id = saleItem.Data.Id;
        var response = await _client.DeleteAsync($"/api/SalesItems/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var getResponse = await _client.GetAsync($"/api/SalesItems/{id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

}

