
namespace M365UK.Functions {
    public class AppSettings {
        public string ClientAppId { get; set; }
        public string ClientAppSecret { get; set; }
        public string TenantId { get; set; }
        public bool ClearTokenCache { get; set; }
    }
}

//* Sample local.settings.json

/*

{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AppSettings:ClientAppId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "AppSettings:ClientAppSecret": "**********************************",
    "AppSettings:TenantId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "AppSettings:ClearTokenCache": "false",
    "AzureWebJobsStorage": "UseDevelopmentStorage=true"
  }
}

*/