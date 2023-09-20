using Azure;
using Azure.Messaging.EventGrid;
using System;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp;

public static class EventGridStatic
{
    public static async Task EventGridTopic(string topicEndpoint, string topicKey, string eventGrid)
    {
        var client = new EventGridPublisherClient(new Uri(topicEndpoint), new AzureKeyCredential(topicKey));

        var binaryData = new BinaryData("Hello, Event Grid!");

        try
        {
            // Add EventGridEvents to a list to publish to the topic
            EventGridEvent eventsList =
                // EventGridEvent with custom model serialized to JSON
                new EventGridEvent(
                    "ExampleEventSubject",
                    "Example.EventType",
                    "1.0",
                    new EventMessage() { Message = $"Cosmos DB NO Sql is {eventGrid}." });
    
            await client.SendEventAsync(eventsList);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}
