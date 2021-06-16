using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Options;
using M365UK.Functions.Interfaces;
using System.Net.Http.Headers;
using System.Collections.Generic;
using M365UK.Functions.Model;
namespace M365UK.Functions
{
    public class Manager
    {
        private readonly ILogger _log;
        private readonly AppSettings _settings;
        private readonly HttpClient _httpClient;
        private readonly IAuthProvider _authProvider;
        private readonly IEnumerable<IAuthProvider> _authProviders;

        public Manager(
            ILoggerFactory loggerFactory,
            IOptions<AppSettings> options,
            HttpClient httpClient,
            IAuthProvider authProvider,
            IEnumerable<IAuthProvider> authProviders)
        {

            _log = loggerFactory.CreateLogger<Manager>();
            _settings = options.Value;
            _httpClient = httpClient;
            _authProvider = authProvider;
            _authProviders = authProviders;
        }


        [FunctionName("GetGroupsUsingADAL")]
        public async Task<IActionResult> GetGroupsUsingADAL(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            _log.LogInformation("--- using ADAL ---");
            var accessToken = await _authProviders.ElementAt(0).GetAccessToken();
            var groups = await GetGroups(accessToken);
            return new OkObjectResult(groups);
        }

        [FunctionName("GetGroupsUsingMSAL")]
        public async Task<IActionResult> GetGroupsUsingMSAL(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            _log.LogInformation("--- using MSAL ---");
            var accessToken = await _authProviders.ElementAt(1).GetAccessToken();
            var groups = await GetGroups(accessToken);
            return new OkObjectResult(groups);
        }

        [FunctionName("GetGroupsUsingAppAuthLibrary")]
        public async Task<IActionResult> GetGroupsUsingAppAuthLibrary(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            _log.LogInformation("--- using App Auth Library ---");
            var accessToken = await _authProviders.ElementAt(2).GetAccessToken();
            var groups = await GetGroups(accessToken);
            return new OkObjectResult(groups);
        }

        [FunctionName("GetGroupsUsingAzureIdentity")]
        public async Task<IActionResult> GetGroupsUsingAzureIdentity(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            _log.LogInformation("--- using Azure.Identity ---");
            var accessToken = await _authProviders.ElementAt(3).GetAccessToken();
            var groups = await GetGroups(accessToken);
            return new OkObjectResult(groups);
        }
        private async Task<List<Value>> GetGroups(string accessToken)
        {
            if (accessToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                                new AuthenticationHeaderValue("Bearer", accessToken);
                Groups groups = await _httpClient.GetFromJsonAsync<Groups>("https://graph.microsoft.com/v1.0/groups?$select=displayName");
                return groups?.value;
            }
            return null;
        }
    }
}
