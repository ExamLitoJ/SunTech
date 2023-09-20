using System;

namespace SunTech.Customer.FuncApp.Requests;

public class CustomerInsertRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime Birthday { get; set; }
}
