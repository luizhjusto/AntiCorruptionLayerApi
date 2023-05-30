using AntiCorruptionLayer.Domain;
using AntiCorruptionLayer.Domain.Helpers;
using AntiCorruptionLayer.Domain.Interfaces;
using AntiCorruptionLayer.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AntiCorruptionLayer.Adapter
{
    public class GitHubAdapter : IGitHubAdapter
    {
        private readonly IGitHubFacade _gitHubFacade;
        public GitHubAdapter(IGitHubFacade gitHubFacade)
        {
            _gitHubFacade = gitHubFacade;
        }

        public async Task<IEnumerable<RepositoryViewModel>> GetRepositoriesAsync()
        {
            /*
             * Convert output data in case of migration system.
             * For exemple, using AutoMapper
             */

            return await _gitHubFacade.GetRepositoriesAsync();
        }

        public async Task CreateRepositoryAsync(RepositoryCreateInputModel input)
        {
            /*
             * Convert input data in case of migration system.
             * For exemple, using AutoMapper
             */

            if (input == null)
                throw new BusinessException("Invalid parameters", HttpStatusCode.BadRequest);

            await _gitHubFacade.CreateRepositoryAsync(input);
        }

        public async Task<IEnumerable<BranchViewModel>> GetBranchesAsync(string repoName)
        {
            /*
            * Convert output data in case of migration system.
            * For exemple, using AutoMapper
            */

            if (string.IsNullOrEmpty(repoName))
                throw new BusinessException("Invalid parameter", HttpStatusCode.BadRequest);

            return await _gitHubFacade.GetBranchesAsync(repoName);
        }

        public async Task<IEnumerable<WebhookViewModel>> GetWebhookAsync(string repoName)
        {
            /*
            * Convert output data in case of migration system.
            * For exemple, using AutoMapper
            */

            if (string.IsNullOrEmpty(repoName))
                throw new BusinessException("Invalid parameter", HttpStatusCode.BadRequest);

            var result = await _gitHubFacade.GetWebhookAsync(repoName);
            if (result == null)
                return Enumerable.Empty<WebhookViewModel>();

            return result;
        }

        public async Task<WebhookCreateViewModel> CreateWebhooksAsync(string repoName, WebhookCreateInputModel input)
        {
            /*
            * Convert input and/or output data in case of migration system.
            * For exemple, using AutoMapper
            */

            if (input == null || string.IsNullOrEmpty(repoName))
                throw new BusinessException("Invalid parameters", HttpStatusCode.BadRequest);

            return await _gitHubFacade.CreateWebhooksAsync(repoName, input);
        }

        public async Task UpdateWebhooksAsync(string repoName, int id, WebhookUpdateInputModel input)
        {
            /*
            * Convert input data in case of migration system.
            * For exemple, using AutoMapper
            */

            if (id == 0 || string.IsNullOrEmpty(repoName))
                throw new BusinessException("Invalid parameters", HttpStatusCode.BadRequest);

            await _gitHubFacade.UpdateWebhooksAsync(repoName, id, input);
        }
    }
}