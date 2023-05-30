using AntiCorruptionLayer.Domain;
using AntiCorruptionLayer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntiCorruptionLayer.Api.Controllers;

[ApiController]
[Route("api/v1/repositories")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubAdapter _gitHubAdapter;
    public GitHubController(IGitHubAdapter gitHubAdapter)
    {
        _gitHubAdapter = gitHubAdapter;
    }

    /// <summary>
    /// Get all repositories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetRepositoriesAsync()
    {
        return Ok(await _gitHubAdapter.GetRepositoriesAsync());
    }

    /// <summary>
    /// Create a new repository on GitHub
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateRepositoryAsync([FromBody] RepositoryCreateInputModel input)
    {
        await _gitHubAdapter.CreateRepositoryAsync(input);
        return Created(string.Empty, null);
    }

    /// <summary>
    /// Get all branches from repository
    /// </summary>
    /// <param name="repoName">Repository name - non-case sensitive</param>
    /// <returns></returns>
    [HttpGet("{repoName}/branches")]
    public async Task<IActionResult> GetBranchesAsync([FromRoute] string repoName)
    {
        return Ok(await _gitHubAdapter.GetBranchesAsync(repoName));
    }

    /// <summary>
    /// Get all webhooks from repository
    /// </summary>
    /// <param name="repoName">Repository name - non-case sensitive</param>
    /// <returns></returns>
    [HttpGet("{repoName}/webhooks")]
    public async Task<IActionResult> GetWebhooksAsync([FromRoute] string repoName)
    {
        return Ok(await _gitHubAdapter.GetWebhookAsync(repoName));
    }

    /// <summary>
    /// Create a webhook for a specific repository
    /// </summary>
    /// <param name="repoName">Repository name - non-case sensitive</param>
    /// <returns></returns>
    [HttpPost("{repoName}/webhooks")]
    public async Task<IActionResult> CreateWebhooksAsync([FromRoute] string repoName, [FromBody] WebhookCreateInputModel input)
    {
        return Created(string.Empty, await _gitHubAdapter.CreateWebhooksAsync(repoName, input));
    }

    /// <summary>
    /// Update a repository wehbook
    /// </summary>
    /// <param name="repoName">Repository name - non-case sensitive</param>
    /// <returns></returns>
    [HttpPut("{repoName}/webhooks/{id}")]
    public async Task<IActionResult> UpdateWebhooksAsync([FromRoute] string repoName, [FromRoute] int id, [FromBody] WebhookUpdateInputModel input)
    {
        await _gitHubAdapter.UpdateWebhooksAsync(repoName, id, input);
        return Ok();
    }        
}