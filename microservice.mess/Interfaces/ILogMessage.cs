using System.Threading.Tasks;
using microservice.mess.Models.Message;

namespace microservice.mess.Interfaces
{
    public interface ILogMessage
    {
        Task InsertAsync(MessageLog log);
        Task InsertErrorAsync(MessageErrorLog errorLog);
    }
}
