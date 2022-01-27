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
        public async Task<SendMessageResponse> SendMessageAsync(AmazonSQSConfig sqsConfig, string qUrl, string message)
        {        
            var sqsClient = new AmazonSQSClient(sqsConfig);
            SendMessageResponse responseSendMsg = await sqsClient.SendMessageAsync(qUrl, message);
            return responseSendMsg;
        }
    }
}
