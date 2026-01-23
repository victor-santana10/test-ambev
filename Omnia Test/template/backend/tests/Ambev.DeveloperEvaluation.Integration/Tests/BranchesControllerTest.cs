using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.Branch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Tests;

public class BranchesControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public BranchesControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // TODO: Create more tests to cover all scenarios

    [Fact]
    public async Task POST_api_Branches_Should_Create_Branch()
    {
        var request = new
        {
            name = "Test Branch",
            isActive = true
        };

        var response = await _client.PostAsJsonAsync("/api/Branches", request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
    }

    [Fact]
    public async Task PUT_api_Branches_Should_Update_Branch()
    {
        var createResponse = await _client.PostAsJsonAsync("/api/Branches", new
        {
            name = "Old Branch",
            isActive = true
        });

        var created = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var updateRequest = new
        {
            id,
            name = "Updated Branch",
            isActive = false
        };

        var response = await _client.PutAsJsonAsync($"/api/Branches?id={id}", updateRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_api_Branches_Id_Should_Return_Branch()
    {
        var createResponse = await _client.PostAsJsonAsync("/api/Branches", new
        {
            name = "Get Branch",
            isActive = true
        });

        var created = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.GetAsync($"/api/Branches/{id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var branch = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetBranchResponse>>();

        Assert.NotNull(branch);
        Assert.Equal(id, branch.Data.Id);
        Assert.Equal("Get Branch", branch.Data.Name);
        Assert.True(branch.Data.IsActive);
    }

    [Fact]
    public async Task DELETE_api_Branches_Id_Should_Delete_Branch()
    {
        var createResponse = await _client.PostAsJsonAsync("/api/Branches", new
        {
            name = "Delete Branch",
            isActive = true
        });

        var created = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Data.Id);

        var id = created.Data.Id;
        var response = await _client.DeleteAsync($"/api/Branches/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var getResponse = await _client.GetAsync($"/api/Branches/{id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

}

