using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp.Functions;

internal class HttpFunctionExecutor : IHttpFunctionExecutor
{
    public async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> func)
    {
        try
        {
            return await func();
        }
        catch (ValidationException ex)
        {
            var result = new
            {
                message = "Validation failed.",
            };

            return new BadRequestObjectResult(result);
        }
    }
}
