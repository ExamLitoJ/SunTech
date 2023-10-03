using Newtonsoft.Json;
using System;

namespace SunTech.Customer.FuncApp.Models;

internal class CustomerModel
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
}
