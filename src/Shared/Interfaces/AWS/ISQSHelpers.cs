using Amazon.SQS.Model;
using System.Threading.Tasks;

namespace Shared.Interfaces.AWS
{
    public interface ISQSHelpers
    {
        Task<SendMessageResponse> SendMessageAsync(string qUrl, string messageBody);
    }
}