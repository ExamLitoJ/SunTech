using MediatR;
using SunTech.Customer.FuncApp.Requests;

namespace SunTech.Customer.FuncApp.Events;

internal class CustomerInsertRecordEvent : INotification
{
    public CustomerInsertRequest CustomerInsertRequest { get; set; }
}