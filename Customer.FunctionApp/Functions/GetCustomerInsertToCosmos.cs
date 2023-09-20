using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SunTech.Customer.FuncApp.Requests;
using SunTech.Customer.FuncApp.Validators;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp.Functions;

internal class GetCustomerInsertToCosmos
{
    private readonly IMediator _mediator;

    private readonly IHttpFunctionExecutor _httpFunctionExecutor;


    public GetCustomerInsertToCosmos(IMediator mediator
        , IHttpFunctionExecutor httpFunctionExecutor
        )
    {
        _mediator = mediator;
        _httpFunctionExecutor = httpFunctionExecutor;
    }

    [FunctionName("CustomerInsertToCosmosDB")]
    public async Task<IActionResult> Run(
     [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req
        , ILogger log
        , ExecutionContext context)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var customerInsertRequest = JsonConvert.DeserializeObject<CustomerInsertRequest>(requestBody);

        if (customerInsertRequest == null)
        {
            return new BadRequestObjectResult("Invalid JSON!!!");
        }

        // Validating
        var validator = new CustomerInsertRequestValidator();
        var validationResult = await validator.ValidateAsync(customerInsertRequest);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult.Errors.Select(e => new
            {
                e.ErrorCode,
                e.PropertyName,
                e.ErrorMessage
            }));
        }

        return await _httpFunctionExecutor.ExecuteAsync(async () =>
        {
            return new OkObjectResult(await _mediator.Send(new Commands.CommandManager.InsertRecordCommand(customerInsertRequest)));
        });
    }
}