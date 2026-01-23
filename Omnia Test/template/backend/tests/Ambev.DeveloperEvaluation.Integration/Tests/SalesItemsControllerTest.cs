using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Bogus.DataSets;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Tests;

public class SalesControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SalesControllerTests(CustomWebApplicationFactory factory)
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

    private async Task<ApiResponseWithData<CreateSaleResponse>> CreateSales(string name, string email, string nameBranch)
    {
        var request = new
        {
            userId = await GetUser(name, email),
            branchId = await GetBranch(nameBranch),
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
        return result;
    }

    [Fact]
    public async Task POST_api_Sales_Should_Create_Sale()
    {
        var result = await CreateSales("Test Create User", "tcu@tcu.com", "Test Create Branch");
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
    }

    [Fact]
    public async Task PUT_api_Sales_Should_Update_Sale()
    {
        var created = await CreateSales("Test Update User", "tuu@tuu.com", "Test Update Branch");
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var updateRequest = new
        {
            id,
            userId = await GetUser("Test New Update User", "tnuu@tnuu.com"),
            branchId = await GetBranch("Test New Update Branch"),
            isActive = false,
            Total = 155
        };

        var response = await _client.PutAsJsonAsync($"/api/Sales?id={id}", updateRequest);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateSaleResponse>>();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_api_Sales_Id_Should_Return_Sale()
    {
        var created = await CreateSales("Test Get Sale", "tgs@tgs.com", "Test Branch Get Sale");
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.GetAsync($"/api/Sales/{id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var sale = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetSaleResponse>>();

        Assert.NotNull(sale);
        Assert.Equal(id, sale.Data.Id);
        Assert.Equal(720, sale.Data.Total);
        Assert.True(sale.Data.IsActive);
    }

    [Fact]
    public async Task GET_All_api_Sales_Should_Return_List_Sale()
    {
        var created = await CreateSales("Test GetAll Sale", "tgAlls@tgs.com", "Test Branch GetAll Sale"); 
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.GetAsync($"/api/Sales");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var sales = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetAllSaleResponse>>();
        Assert.NotNull(sales);
        Assert.True(sales.Data.Sales.Exists(x => x.Id.Equals(id)));
    }

    [Fact]
    public async Task DELETE_api_Sales_Id_Should_Delete_Sale()
    {
        var created = await CreateSales("Test Delete Sale", "tds@tds.com", "Test Branch Delete Sale");
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.DeleteAsync($"/api/Sales/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var getResponse = await _client.GetAsync($"/api/Sales/{id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

}

