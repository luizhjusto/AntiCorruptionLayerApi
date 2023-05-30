using System.Collections.Generic;
using System.Threading.Tasks;
using AntiCorruptionLayer.Domain.ViewModel;

namespace AntiCorruptionLayer.Domain.Interfaces
{
    public interface IGitHubFacade
    {
        Task<IEnumerable<RepositoryViewModel>> GetRepositoriesAsync();
        Task CreateRepositoryAsync(RepositoryCreateInputModel input);

        Task<IEnumerable<BranchViewModel>> GetBranchesAsync(string repoName);

        Task<IEnumerable<WebhookViewModel>> GetWebhookAsync(string repoName);

        Task<WebhookCreateViewModel> CreateWebhooksAsync(string repoName, WebhookCreateInputModel input);

        Task UpdateWebhooksAsync(string repoName, int id, WebhookUpdateInputModel input);
    }
}