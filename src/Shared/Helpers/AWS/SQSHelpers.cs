using Amazon.SQS;
using Amazon.SQS.Model;
using Shared.Interfaces.AWS;
using System;
using System.Threading.Tasks;

namespace Shared.Helpers.AWS
{
    public class SQSHelpers : ISQSHelpers
    {

        // Method to put a message on a queue
        public async Task<SendMessageResponse> SendMessageAsync(string qUrl, string messageBody)
        {
            var sqsClient = new AmazonSQSClient();
            SendMessageResponse responseSendMsg = await sqsClient.SendMessageAsync(qUrl, messageBody);
            Console.WriteLine($"Message {responseSendMsg.MessageId} added to queue\n  {qUrl}");
            Console.WriteLine($"HttpStatusCode: {responseSendMsg.HttpStatusCode}");
            return responseSendMsg;
        }
    }
}
