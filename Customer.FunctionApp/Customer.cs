using Newtonsoft.Json;
using System;

namespace SunTech.Customer.FuncApp;

internal class Customer
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "partitionKey")]
    public string PartitionKey { get; set; }

    [JsonProperty(PropertyName = "FirstName")]
    public string FirstName { get; set; }

    [JsonProperty(PropertyName = "LastName")]
    public string LastName { get; set; }

    [JsonProperty(PropertyName = "Email")]
    public string Email { get; set; }

    [JsonIgnore]
    public DateTime Birthday { get; set; }

    [JsonProperty(PropertyName = "BirthdayInEpoch")]
    public string BirthdayInEpoch { get; set; }

    public DateTime ConvertBirthdayEpochToDate()
    {
        double timestamp = Convert.ToInt32(BirthdayInEpoch);
        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0); //from start epoch time
        return start.AddSeconds(timestamp);
    }

    public string ConvertBirthdayToEpoch()
    {
        TimeSpan t = Birthday - new DateTime(1970, 1, 1);
        return ((int)t.TotalSeconds).ToString();
    }

    public string CreatePartitionKey() => Guid.NewGuid().ToString();
}
