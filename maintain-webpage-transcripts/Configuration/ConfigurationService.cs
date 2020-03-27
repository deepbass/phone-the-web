using System;
using Newtonsoft.Json;

public static class ConfigurationService
{
    public static ApplicationConfiguration GetConfiguration()
    {
        return new ApplicationConfiguration()
        {
            PhoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER"),
            PermittedDomains = JsonConvert.DeserializeObject<string[]>(Environment.GetEnvironmentVariable("PERMITTED_DOMAINS"))
        };
    }
}