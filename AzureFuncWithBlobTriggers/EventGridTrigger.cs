// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureEventGrid.Function
{
    public class EventGridTrigger
    {
        private readonly ILogger<EventGridTrigger> _logger;

        public EventGridTrigger(ILogger<EventGridTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(EventGridTrigger))]
        public async Task Run([BlobTrigger("PathValue/{name}", Source = BlobTriggerSource.EventGrid, Connection = "ConnectionValue")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob Trigger (using Event Grid) processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
