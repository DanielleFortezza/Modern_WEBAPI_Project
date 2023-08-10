using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebAPI.Services.GitHubService
{
    public class GitHubService : IGitHubService
    {
        IConfiguration Configuration;

        public GitHubService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<List<GitRepositoryModel>> GetReprositories()
        {
            List<GitRepositoryModel> GitRepos = new List<GitRepositoryModel>();

            using (HttpClient HttpClient = new HttpClient())
            {
                HttpClient.BaseAddress = new Uri(Configuration.GetValue<string>("GitHubBaseApi"));
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpClient.DefaultRequestHeaders.Add("Accept", Configuration.GetValue<string>("GitHubAcceptHeader"));
                HttpClient.DefaultRequestHeaders.Add("User-Agent", Configuration.GetValue<string>("MyUserAgent"));
                var response = await HttpClient.GetAsync(Configuration.GetValue<string>("GitHubGetReposUri"));

                if (response.IsSuccessStatusCode)
                {
                    GitRepos = response.Content.ReadAsAsync<List<GitRepositoryModel>>().Result;
                } else
                {
                    return null;
                }
            }
            return GitRepos;
        }
    }
}
