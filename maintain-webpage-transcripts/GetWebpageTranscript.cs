using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;

namespace PhoneTheWeb.MaintainWebpageTranscripts
{
    public static class GetWebpageTranscript
    {
        [FunctionName("GetWebpageTranscript")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "webpagetranscripts/{urlHash}")] HttpRequest req,
            [Table("WebPageTranscripts")] CloudTable webPageTranscriptTable,
            string urlHash,
            ILogger log)
        {
            var config = ConfigurationService.GetConfiguration();
            var webPageTranscriptService = new WebPageTranscriptService(config, webPageTranscriptTable);
            var webPageTranscript = await webPageTranscriptService.Get(urlHash);

            if (webPageTranscript == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(webPageTranscript);
        }
    }
}
