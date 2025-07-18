using microservice.mess.Models;
using microservice.mess.Models.Message;

namespace microservice.mess.Interfaces
{
    public interface IMailTemplate
    {
        Task CreateAsync(AllMessageTemplate template);
        Task<List<AllMessageTemplate>> GetAllAsync();
        Task<AllMessageTemplate?> GetByNameAsync(string name);
    }
}
