using System.Threading.Tasks;

namespace PROJECT.BASE.SERVICES
{
    public interface ISmsService
    {
        Task<GetAccountBalanceResponse> GetAccountBalanceAsync();
        Task<SendSmsResponse> SendSmsAsync(SendSmsRequest request);
    }
}