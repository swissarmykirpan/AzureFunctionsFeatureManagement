using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace FunctionApp1
{

    public class Function1
    {
        private readonly IFeatureManager _featureManager;

        public Function1(IFeatureManagerSnapshot featureManager)
        {
            _featureManager = featureManager;
        }

        [FunctionName("Function1")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var isEnabled = _featureManager.IsEnabled("nonExistentFeature");

            return new OkObjectResult($"nonExistentFetature enabled status: {isEnabled}");
        }
    }
}
