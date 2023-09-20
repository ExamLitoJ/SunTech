using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SunTech.Customer.FuncApp.Events;
using SunTech.Customer.FuncApp.Options;
using System.Threading;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp.Notifications;

internal class CustomerInsertRecordEventHandler : INotificationHandler<CustomerInsertRecordEvent>
{
    private readonly EventGridOptions eventGridOptions;
    public CustomerInsertRecordEventHandler(IOptions<EventGridOptions> options)
    {
        eventGridOptions = options.Value;
    }
    public async Task Handle(CustomerInsertRecordEvent notification, CancellationToken cancellationToken)
    {
        await EventGridStatic.EventGridTopic(eventGridOptions.TopicEndpoint
            , eventGridOptions.TopicAccessKey
            , JsonConvert.SerializeObject(notification.CustomerInsertRequest));  
    }
}