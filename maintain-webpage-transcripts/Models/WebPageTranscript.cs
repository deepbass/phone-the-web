using System;
using Microsoft.WindowsAzure.Storage.Table;

public class WebPageTranscript
{
    public string Url { get; set; }
    public string UrlHash { get; set; }
    public string Host { get; set; }
    public string Transcript { get; set; }
    public int ReferenceNumber { get; set; }
    public string PhoneNumber { get; set; }
}
public class WebPageTranscriptEntity : TableEntity
{
    public string Url { get; set; }
    public string UrlHash { get; set; }
    public string Host { get; set; }
    public string Transcript { get; set; }
    public int ReferenceNumber { get; set; }
    public string PhoneNumber { get; set; }
}