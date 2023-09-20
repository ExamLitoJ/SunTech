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
    internal class InsertRecordCommand : IRequest<CommandResponse>
    {
        private readonly CustomerInsertRequest _insertRequest;

        public InsertRecordCommand(CustomerInsertRequest insertRequest)
        {
            _insertRequest = insertRequest;
        }

        public class InsertRecordCommandHandler : IRequestHandler<InsertRecordCommand, CommandResponse>
        {
            private readonly ICosmosDBReposity _cosmosDBReposity;

            private readonly IPublisher _publisher;
            public InsertRecordCommandHandler(ICosmosDBReposity cosmosDBReposity, IPublisher publisher)
            {
                _cosmosDBReposity = cosmosDBReposity;
                _publisher = publisher;
            }

            public async Task<CommandResponse> Handle(InsertRecordCommand request, CancellationToken cancellationToken)
            {
                var results = await _cosmosDBReposity.CustomerInsertRecordAsync(request._insertRequest);
                if (results.IsSuccess)
                {
                    await _publisher.Publish(new CustomerInsertRecordEvent
                    {
                        CustomerInsertRequest = request._insertRequest
                    }, cancellationToken);
                }
                return results;
            }
        }
    }

}