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
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PhoneTheWeb.MaintainWebpageTranscripts
{
    public static class CreateWebPageTranscript
    {
        [FunctionName("CreateWebPageTranscript")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "webpagetranscripts")] HttpRequest req,
            [Table("WebPageTranscripts")] CloudTable webPageTranscriptTable,
            ILogger log)
        {
            var webPageTranscript = JsonConvert.DeserializeObject<WebPageTranscript>(await req.ReadAsStringAsync());
            log.LogInformation("Creating new WebPageTranscript");
            var config = ConfigurationService.GetConfiguration();
            var webPageTranscriptService = new WebPageTranscriptService(config,webPageTranscriptTable);
            var urlHash = await webPageTranscriptService.CreateWebPageTranscript(webPageTranscript);
            return new OkObjectResult(await webPageTranscriptService.Get(urlHash));
        }

        
    }
}
