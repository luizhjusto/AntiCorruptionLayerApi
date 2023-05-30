using Xunit;
using AntiCorruptionLayer.Api.Controllers;
using AntiCorruptionLayer.Domain;
using AntiCorruptionLayer.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AntiCorruptionLayer.Test
{
    public class GitHubControllerTest : IClassFixture<GitHubController>
    {
        private readonly GitHubController _gitHubController;
        private readonly string _repoName = "<RepoName>";
        public GitHubControllerTest(GitHubController gitHubController)
        {
            _gitHubController = gitHubController;
        }

        [Fact]
        public async Task TestGetRepositories()
        {
            var result = await _gitHubController.GetRepositoriesAsync() as ObjectResult;

            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.NotNull(result?.Value);
            Assert.IsAssignableFrom<IEnumerable<RepositoryViewModel>>(result?.Value);
        }

        [Fact]
        public async void TestCreateRepository()
        {
            var newRepository = new RepositoryCreateInputModel()
            {
                Name = "test",
                Description = "test",
                Private = false,
                AutoInit = true
            };

            var result = await _gitHubController.CreateRepositoryAsync(newRepository) as ObjectResult;
            
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Null(result?.Value);
            Assert.Equal(201, result?.StatusCode);
        }

        [Fact]
        public async Task TestGetBranches()
        {
            var result = await _gitHubController.GetBranchesAsync(_repoName) as ObjectResult;

            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.NotNull(result?.Value);
            Assert.IsAssignableFrom<IEnumerable<BranchViewModel>>(result?.Value);            
        }

        [Fact]
        public async Task TestGetWebhooks()
        {
            var result = await _gitHubController.GetWebhooksAsync(_repoName) as ObjectResult;

            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.NotNull(result?.Value);
            Assert.IsAssignableFrom<IEnumerable<WebhookViewModel>>(result?.Value);
        }

        [Fact]
        public async Task TestCreateWebhooks()
        {
            var newWebhook = new WebhookCreateInputModel()
            {
                Name = "web",
                Active = true,
                Events = new List<string>() { "push", "pull_request" },
                Config = new WebhookConfigInputModel()
                {
                    Url = "https://example.com/webhook",
                    ContentType = "json",
                    InsecureSsl = "0"
                }
            };

            var result = await _gitHubController.CreateWebhooksAsync(_repoName, newWebhook) as ObjectResult;

            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.NotNull(result?.Value);
            Assert.True(((WebhookCreateViewModel)result.Value).Id > 0, "Id is not greater than 0");
            Assert.Equal(201, result?.StatusCode);
        }

        [Fact]
        public async Task TestUpdateWebhooks()
        {
            var hookId = 417093139; /* This id needs to be updated according to your repository webhook's id. */
            var newWebhook = new WebhookUpdateInputModel()
            {
                Active = true,
                Events = new List<string>() { "push", "pull_request" },
                Config = new WebhookConfigInputModel()
                {
                    Url = "https://example.com/webhook/test3",
                    ContentType = "json",
                    InsecureSsl = "0"
                }
            };

            var result = await _gitHubController.UpdateWebhooksAsync(_repoName, hookId, newWebhook) as OkResult;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(200, result?.StatusCode);
        }
    }
}