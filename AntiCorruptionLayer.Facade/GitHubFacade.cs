using AntiCorruptionLayer.Domain;
using AntiCorruptionLayer.Domain.Helpers;
using AntiCorruptionLayer.Domain.Interfaces;
using AntiCorruptionLayer.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AntiCorruptionLayer.Facade
{
    public class GitHubFacade : IGitHubFacade
    {
        private readonly IHTTPRequestGeneric _hTTPRequestGeneric;

        public GitHubFacade(IHTTPRequestGeneric hTTPRequestGeneric)
        {
            _hTTPRequestGeneric = hTTPRequestGeneric;
        }

        public async Task<IEnumerable<RepositoryViewModel>> GetRepositoriesAsync()
        {
            var result = await _hTTPRequestGeneric.Request<IEnumerable<RepositoryViewModel>>(new Uri($"{Config.GitHubBaseUrl}/user/repos"),
                HttpMethod.Get,
                payload: null,
                new Dictionary<string, string>()
                {
                    { "User-Agent", "AntiCorruptionLayer" },
                    { "Accept", "application/vnd.github+json" },
                    { "Authorization", $"Bearer {Config.GitHubBearer}" }
                });

            return result;
        }

        public async Task CreateRepositoryAsync(RepositoryCreateInputModel input)
        {
            await _hTTPRequestGeneric.Request<dynamic>(new Uri($"{Config.GitHubBaseUrl}/user/repos"),
                HttpMethod.Post,
                input,
                new Dictionary<string, string>()
                {
                    { "User-Agent", "AntiCorruptionLayer" },
                    { "Accept", "application/vnd.github+json" },
                    { "Authorization", $"Bearer {Config.GitHubBearer}" }
                });
        }

        public async Task<IEnumerable<BranchViewModel>> GetBranchesAsync(string repoName)
        {
            var result = await _hTTPRequestGeneric.Request<IEnumerable<BranchViewModel>>(new Uri($"{Config.GitHubBaseUrl}/repos/{Config.GitHubOwner}/{repoName}/branches"),
                HttpMethod.Get,
                payload: null,
                new Dictionary<string, string>()
                {
                    { "User-Agent", "AntiCorruptionLayer" },
                    { "Accept", "application/vnd.github+json" },
                    { "Authorization", $"Bearer {Config.GitHubBearer}" }
                });

            return result;
        }

        public async Task<IEnumerable<WebhookViewModel>> GetWebhookAsync(string repoName)
        {
            var result = await _hTTPRequestGeneric.Request<IEnumerable<WebhookViewModel>>(
                new Uri($"{Config.GitHubBaseUrl}/repos/{Config.GitHubOwner}/{repoName}/hooks"),
                HttpMethod.Get,
                payload: null,
                new Dictionary<string, string>()
                {
                    { "User-Agent", "AntiCorruptionLayer" },
                    { "Accept", "application/vnd.github+json" },
                    { "Authorization", $"Bearer {Config.GitHubBearer}" }
                });

            return result;
        }

        public async Task<WebhookCreateViewModel> CreateWebhooksAsync(string repoName, WebhookCreateInputModel input)
        {
            return await _hTTPRequestGeneric.Request<WebhookCreateViewModel>(new Uri($"{Config.GitHubBaseUrl}/repos/{Config.GitHubOwner}/{repoName}/hooks"),
                HttpMethod.Post,
                input,
                new Dictionary<string, string>()
                {
                    { "User-Agent", "AntiCorruptionLayer" },
                    { "X-GitHub-Api-Version", "2022-11-28" },
                    { "Accept", "*/*" },
                    { "Authorization", $"Bearer {Config.GitHubBearer}" }
                });
        }

        public async Task UpdateWebhooksAsync(string repoName, int id, WebhookUpdateInputModel input)
        {
            await _hTTPRequestGeneric.Request<dynamic>(new Uri($"{Config.GitHubBaseUrl}/repos/{Config.GitHubOwner}/{repoName}/hooks/{id}"),
                HttpMethod.Patch,
                input,
                new Dictionary<string, string>()
                {
                    { "User-Agent", "AntiCorruptionLayer" },
                    { "Accept", "application/vnd.github+json" },
                    { "Authorization", $"Bearer {Config.GitHubBearer}" }
                });
        }
    }
}