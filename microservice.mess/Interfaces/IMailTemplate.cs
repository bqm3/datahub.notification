using microservice.mess.Models;
using microservice.mess.Models.Message;

namespace microservice.mess.Interfaces
{
    public interface IMailTemplate
    {
        Task CreateAsync(MailTemplateModel template);
        Task<List<MailTemplateModel>> GetAllAsync();
        Task<MailTemplateModel?> GetByTagAsync(string tag);
    }
}
