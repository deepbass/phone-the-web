public static class WebPageTranscriptMapper
{
    public static WebPageTranscriptEntity mapToEntity(WebPageTranscript webPageTranscript) => new WebPageTranscriptEntity()
    {
        Transcript = webPageTranscript.Transcript,
        ReferenceNumber = webPageTranscript.ReferenceNumber,
        PhoneNumber = webPageTranscript.PhoneNumber,
        Url = webPageTranscript.Url,
        UrlHash = webPageTranscript.UrlHash,
        Host = webPageTranscript.Host,
        RowKey = webPageTranscript.UrlHash.ToString(),
        PartitionKey = webPageTranscript.Host
    };

    public static WebPageTranscript mapFromEntity(WebPageTranscriptEntity webPageTranscriptEntity) => new WebPageTranscript(){
        Transcript = webPageTranscriptEntity.Transcript,
        ReferenceNumber = webPageTranscriptEntity.ReferenceNumber,
        PhoneNumber = webPageTranscriptEntity.PhoneNumber,
        Url = webPageTranscriptEntity.Url,
        UrlHash = webPageTranscriptEntity.UrlHash,
        Host = webPageTranscriptEntity.Host
    };
}