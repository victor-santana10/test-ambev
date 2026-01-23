using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Tests;

public class ProductsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // TODO: Create more tests to cover all scenarios

    [Fact]
    public async Task POST_api_Products_Should_Create_Product()
    {
        var request = new
        {
            name = "Test Product",
            Price = 200,
            isActive = true
        };

        var response = await _client.PostAsJsonAsync("/api/Products", request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
    }

    [Fact]
    public async Task PUT_api_Products_Should_Update_Product()
    {
        var createResponse = await _client.PostAsJsonAsync("/api/Products", new
        {
            name = "Old Product",
            Price = 200,
            isActive = true
        });

        var created = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var updateRequest = new
        {
            id,
            name = "Updated Product",
            Price = 300,
            isActive = false
        };

        var response = await _client.PutAsJsonAsync($"/api/Products?id={id}", updateRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_api_Products_Id_Should_Return_Product()
    {
        var createResponse = await _client.PostAsJsonAsync("/api/Products", new
        {
            name = "Get Product",
            Price = 200,
            isActive = true
        });

        var created = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.GetAsync($"/api/Products/{id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var product = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetProductResponse>>();

        Assert.NotNull(product);
        Assert.Equal(id, product.Data.Id);
        Assert.Equal("Get Product", product.Data.Name);
        Assert.Equal(200, product.Data.Price);
        Assert.True(product.Data.IsActive);
    }

    [Fact]
    public async Task GET_All_api_Products_Should_Return_List_Product()
    {
        var createResponse = await _client.PostAsJsonAsync("/api/Products", new
        {
            name = "Get All Product",
            Price = 200,
            isActive = true
        });

        var created = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.GetAsync($"/api/Products");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var product = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetAllProductResponse>>();

        Assert.NotNull(product);
        Assert.True(product.Data.Products.Exists(x => x.Id.Equals(id)));
    }

    [Fact]
    public async Task DELETE_api_Products_Id_Should_Delete_Product()
    {
        var createResponse = await _client.PostAsJsonAsync("/api/Products", new
        {
            name = "Delete Product",
            Price = 200,
            isActive = true
        });

        var created = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.DeleteAsync($"/api/Products/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var getResponse = await _client.GetAsync($"/api/Products/{id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

}

