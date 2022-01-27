using Amazon.SQS;
using Amazon.SQS.Model;
using System.Threading.Tasks;

namespace Shared.Interfaces.AWS
{
    public interface ISQSHelpers
    {
        Task<SendMessageResponse> SendMessageAsync(AmazonSQSConfig sqsConfig, string qUrl, string message);
    }
}