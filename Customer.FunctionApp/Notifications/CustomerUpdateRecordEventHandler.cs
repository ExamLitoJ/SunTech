using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SunTech.Customer.FuncApp.Events;
using SunTech.Customer.FuncApp.Options;
using System.Threading;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp.Notifications;

internal class CustomerUpdateRecordEventHandler : INotificationHandler<CustomerUpdateRecordEvent>
{
    private readonly EventGridOptions eventGridOptions;
    public CustomerUpdateRecordEventHandler(IOptions<EventGridOptions> options)
    {
        eventGridOptions = options.Value;
    }
    public async Task Handle(CustomerUpdateRecordEvent notification, CancellationToken cancellationToken)
    {
        await EventGridStatic.EventGridTopic(eventGridOptions.TopicEndpoint
            , eventGridOptions.TopicAccessKey
            , JsonConvert.SerializeObject(notification.CustomerUpdateRequest));
    }
}
