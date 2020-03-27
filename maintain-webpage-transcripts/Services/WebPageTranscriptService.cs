using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

public class WebPageTranscriptService
{
    private CloudTable _cloudTable;
    private ApplicationConfiguration _appConfig;
    public WebPageTranscriptService(ApplicationConfiguration appConfig, CloudTable cloudTable)
    {
        this._cloudTable = cloudTable;
        this._appConfig = appConfig;
    }

    public async Task<WebPageTranscript> Get(string urlHash)
    {
        TableQuery<WebPageTranscriptEntity> getByUrlHash = new TableQuery<WebPageTranscriptEntity>()
            .Where(
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, urlHash)
            );
        WebPageTranscript webPageTranscript = null;
        foreach (WebPageTranscriptEntity webPageTranscriptEntity in await this._cloudTable.ExecuteQuerySegmentedAsync(getByUrlHash, null))
        {
            webPageTranscript = WebPageTranscriptMapper.mapFromEntity(webPageTranscriptEntity);
        }
        return webPageTranscript;
    }

    public async Task<string> CreateWebPageTranscript(WebPageTranscript webPageTranscript)
    {
        webPageTranscript.Transcript = webPageTranscript.Transcript ?? string.Empty;
        webPageTranscript.Host = new Uri(webPageTranscript.Url).Host;
        if (!this._appConfig.PermittedDomains.Contains(webPageTranscript.Host)) throw new Exception("Not allowed");
        TableQuery<WebPageTranscriptEntity> getByPartition = new TableQuery<WebPageTranscriptEntity>().Where(
            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                    webPageTranscript.Host));
        var results = await this._cloudTable.ExecuteQuerySegmentedAsync(getByPartition, null);
        var highestIdSoFar = results.Count() > 0 ? results.Max(x => x.ReferenceNumber) : 0;
        webPageTranscript.ReferenceNumber = highestIdSoFar + 1;
        webPageTranscript.PhoneNumber = this._appConfig.PhoneNumber;
        using (SHA256 sha256Hash = SHA256.Create())
        {
            webPageTranscript.UrlHash = GetHash(sha256Hash, webPageTranscript.Url);
        }
        var entity = WebPageTranscriptMapper.mapToEntity(webPageTranscript);
        TableOperation insertOrMergeOperation = TableOperation.InsertOrReplace(entity);
        await this._cloudTable.ExecuteAsync(insertOrMergeOperation);
        return entity.UrlHash;
    }
    private string GetHash(HashAlgorithm hashAlgorithm, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        var sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
}