//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using SunTech.Customer.FuncApp.Requests;
//using System.IO;
//using System.Threading.Tasks;

//namespace SunTech.Customer.FuncApp;

//public static class InsertCosmosData
//{
//    [FunctionName("InsertCosmosData")]
//    public static async Task<IActionResult> Run(
//     [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req
//        , ILogger log
//        , ExecutionContext context)
//    {
//        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//        var insertRequest = JsonConvert.DeserializeObject<CustomerInsertRequest>(requestBody);

//        if (insertRequest == null)
//        {
//            return new BadRequestObjectResult("Invalid JSON!!!");
//        }

//        return new OkObjectResult(await _mediator.Send(new Commands.CommandManager.InsertRecordCommand(insertRequest)));
//    }
//}
