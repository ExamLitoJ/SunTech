using MediatR;
using SunTech.Customer.FuncApp.Requests;

namespace SunTech.Customer.FuncApp.Events;

internal class CustomerUpdateRecordEvent : INotification
{
    public CustomerUpdateRequest CustomerUpdateRequest { get; set; }
}
