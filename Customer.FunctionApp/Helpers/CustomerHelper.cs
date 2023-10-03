using System;
using SunTech.Customer.FuncApp.Models;

namespace SunTech.Customer.FuncApp.Helpers;

internal static class CustomerHelper
{
    public static DateTime ConvertBirthdayEpochToDate(this CustomerModel customer)
    {
        double timestamp = Convert.ToInt32(customer.BirthdayInEpoch);
        DateTime start = new(1970, 1, 1, 0, 0, 0, 0); //from start epoch time
        return start.AddSeconds(timestamp);
    }

    public  static string ConvertBirthdayToEpoch(this CustomerModel customer)
    {
        TimeSpan t = customer.Birthday - new DateTime(1970, 1, 1);
        return ((int)t.TotalSeconds).ToString();
    }

    public static string CreateId()
    {
        Random random = new(DateTime.Now.Millisecond);
        return random.Next().ToString();
    }

    public static string CreatePartitionKey() => Guid.NewGuid().ToString();
}
