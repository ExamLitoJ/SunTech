using System;

namespace SunTech.Customer.FuncApp.Requests;

internal class CustomerUpdateRequest
{
    public string Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime Birthday { get; set; }

    public string PartitionKey { get; set; }

}
