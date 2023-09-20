using MediatR;
using SunTech.Customer.FuncApp.Events;
using SunTech.Customer.FuncApp.Repository;
using SunTech.Customer.FuncApp.Requests;
using SunTech.Customer.FuncApp.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp.Commands;

internal static partial class CommandManager
{
    internal class UpdateRecordCommand : IRequest<CommandResponse>
    {
        private readonly CustomerUpdateRequest _updateRequest;

        public UpdateRecordCommand(CustomerUpdateRequest updateRequest)
        {
            _updateRequest = updateRequest;
        }

        public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, CommandResponse>
        {
            private readonly ICosmosDBReposity _cosmosDBReposity;

            private readonly IPublisher _publisher;
            public UpdateRecordCommandHandler(ICosmosDBReposity cosmosDBReposity, IPublisher publisher)
            {
                _cosmosDBReposity = cosmosDBReposity;
                _publisher = publisher;
            }

            public async Task<CommandResponse> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
            {
                var results = await _cosmosDBReposity.UpdateRecordAsync(request._updateRequest);
                if (results.IsSuccess)
                    await _publisher.Publish(new CustomerUpdateRecordEvent
                    {
                        CustomerUpdateRequest = request._updateRequest
                    }, cancellationToken);

                return results;
            }
        }
    }

}
