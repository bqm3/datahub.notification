using System.Threading.Tasks;
using microservice.mess.Models.Message;

namespace microservice.mess.Interfaces
{
    public interface ILogMessageRepository
    {
        Task InsertAsync(MessageLog log);
        Task InsertErrorAsync(MessageErrorLog errorLog);
    }
}
