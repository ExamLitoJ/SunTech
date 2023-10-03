using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using SunTech.Customer.FuncApp.Helpers;
using SunTech.Customer.FuncApp.Models;
using SunTech.Customer.FuncApp.Options;
using SunTech.Customer.FuncApp.Requests;
using SunTech.Customer.FuncApp.Response;
using System;
using System.Net;
using System.Threading.Tasks;
using Container = Microsoft.Azure.Cosmos.Container;

namespace SunTech.Customer.FuncApp.Repository;

internal class CosmosDBRepository : ICosmosDBReposity
{
    private readonly CosmosDBConfigurationOptions cosmosDBConfigurationOptions;

    // The Cosmos client instance
    private readonly CosmosClient cosmosClient;

    // The database we will create
    private Database database;

    // The container we will create.
    private Container container;

    public CosmosDBRepository(IOptions<CosmosDBConfigurationOptions> options)
    {
        cosmosDBConfigurationOptions = options.Value;
        cosmosClient = new CosmosClient(cosmosDBConfigurationOptions.CosmosDbEndpoint
            , cosmosDBConfigurationOptions.CosmosDbKey
            , new CosmosClientOptions() { ApplicationName = "SunTechCosmosDB" });
    }
    /// <CustomerInsertRecordAsync>
    /// <summary>
    /// Add CustomerModel item to the container
    /// </summary>
    public async Task<CommandResponse> CustomerInsertRecordAsync(CustomerInsertRequest insertRequest)
    {
        var commandResponse = new CommandResponse();
        try
        {

            CustomerModel customer = new()
            {
                FirstName = insertRequest.FirstName,
                LastName = insertRequest.LastName,
                Birthday = insertRequest.Birthday,
                Email = insertRequest.Email,
                PartitionKey = CustomerHelper.CreatePartitionKey(),
                Id = CustomerHelper.CreateId()
            };
            customer.BirthdayInEpoch = customer.ConvertBirthdayToEpoch();

            await CreateDatabaseAsync();
            await ScaleContainerAsync();
            ItemResponse<CustomerModel> bookingResponse = await this.container.CreateItemAsync<CustomerModel>(customer, new PartitionKey(customer.PartitionKey));
            commandResponse.Message = $"Successfuly inserted new record. To update use this partition Key {customer.PartitionKey}. Id is {customer.Id}";
            commandResponse.IsSuccess = true;
        }
        catch (Exception ex)
        {
            commandResponse.Message = $"Insert failed. Error is {ex.Message}:";
        }

        return commandResponse;
    }

    /// <CustomerUpdateRecordAsync>
    /// <summary>
    /// Add CustomerModel item to the container
    /// </summary>
    public async Task<CommandResponse> UpdateRecordAsync(CustomerUpdateRequest updateRequest)
    {
        var commandResponse = new CommandResponse();
        try
        {
            CustomerModel customer = new()
            {
                FirstName = updateRequest.FirstName,
                LastName = updateRequest.LastName,
                Birthday = updateRequest.Birthday,
                PartitionKey = updateRequest.PartitionKey,
                Email =updateRequest.Email,
                Id = updateRequest.Id
            };
            customer.BirthdayInEpoch = customer.ConvertBirthdayToEpoch();

            await CreateDatabaseAsync();
            ItemResponse<CustomerModel> bookingResponse = await container.ReadItemAsync<CustomerModel>(customer.Id, new PartitionKey(customer.PartitionKey));
            await container.UpsertItemAsync<CustomerModel>(customer);
            commandResponse.Message = $"Successfuly updated record details for Partition Key : {customer.PartitionKey}.";
            commandResponse.IsSuccess = true;
        }
        catch (Exception ex)
        {
            commandResponse.Message = $"Update failed. Error is {ex.Message}:";
        }

        return commandResponse;
    }

    // <CreateDatabaseAsync>
    /// <summary>
    /// Create the database if it does not exist
    /// </summary>
    private async Task CreateDatabaseAsync()
    {
        // Create a new database
        database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosDBConfigurationOptions.CosmosDbDatabaseId);
        await CreateContainerAsync();
    }

    // <CreateContainerAsync>
    /// <summary>
    /// Create the container if it does not exist. 
    /// Specifiy "/partitionKey" as the partition key path since we're storing family information, to ensure good distribution of requests and storage.
    /// </summary>
    /// <returns></returns>
    private async Task CreateContainerAsync()
    {
        // Create a new container
        container = await database.CreateContainerIfNotExistsAsync(cosmosDBConfigurationOptions.CosmosDbContainerId, "/partitionKey");
    }

    // <ScaleContainerAsync>
    /// <summary>
    /// Scale the throughput provisioned on an existing Container.
    /// You can scale the throughput (RU/s) of your container up and down to meet the needs of the workload. Learn more: https://aka.ms/cosmos-request-units
    /// </summary>
    /// <returns></returns>
    private async Task ScaleContainerAsync()
    {
        // Read the current throughput
        try
        {
            int? throughput = await this.container.ReadThroughputAsync();
            if (throughput.HasValue)
            {
                int newThroughput = throughput.Value + 100;
                await this.container.ReplaceThroughputAsync(newThroughput);
            }
        }
        catch (CosmosException cosmosException) when (cosmosException.StatusCode == HttpStatusCode.BadRequest)
        {
            Console.WriteLine("Cannot read container throuthput.");
            Console.WriteLine(cosmosException.ResponseBody);
        }

    }


}
