using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;


namespace WebAPI.Services.GitHubService
{
    public interface IGitHubService
    {
        Task<List<GitRepositoryModel>> GetReprositories();
    }
}
