using System.Threading.Tasks;

namespace PROJECT.BASE.SERVICES
{
    public interface ISlackService
    {
        Task<string> SendMessageAsync(SendSlackMessageRequest request);
    }
}