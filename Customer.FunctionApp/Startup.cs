using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SunTech.Customer.FuncApp.Functions;
using SunTech.Customer.FuncApp.Options;
using SunTech.Customer.FuncApp.Repository;
using System.Reflection;

[assembly: FunctionsStartup(typeof(SunTech.Customer.FuncApp.Startup))]
namespace SunTech.Customer.FuncApp;

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.Configure<CosmosDBConfigurationOptions>(builder.GetContext().Configuration.GetSection("CosmosDBConfigurationOptions"));
        builder.Services.Configure<EventGridOptions>(builder.GetContext().Configuration.GetSection("EventGridOptions"));

        builder.Services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

        builder.Services.AddScoped<ICosmosDBReposity, CosmosDBRepository>();

        builder.Services.AddSingleton<IHttpFunctionExecutor, HttpFunctionExecutor>();

    }
}
