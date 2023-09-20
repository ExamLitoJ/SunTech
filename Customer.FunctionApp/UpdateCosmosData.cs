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

//public static class UpdateCosmosData
//{
//    [FunctionName("UpdateCosmosData")]
//    public static async Task<IActionResult> Run(
//     [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req
//        , ILogger log
//        , ISender sender
//        , ExecutionContext context)
//    {
//        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//        var customerUpdateRequest = JsonConvert.DeserializeObject<CustomerUpdateRequest>(requestBody);

//        if (customerUpdateRequest == null)
//        {
//            return new BadRequestObjectResult("Invalid JSON!!!");
//        }

//        return new OkObjectResult(sender.Send(new Commands.CommandManager.UpdateRecordCommand(customerUpdateRequest)));
//    }
//}
