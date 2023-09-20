using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp.Functions;

internal interface IHttpFunctionExecutor
{
    Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> func);
}
