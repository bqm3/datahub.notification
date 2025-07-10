using microservice.mess.Models;
using microservice.mess.Models.Message;

namespace microservice.mess.Interfaces
{
    public interface ISignetTemplate
    {
        Task CreateSGIActionAsync(SGIActionModel model);
        Task<SgiMessageTemplate?> GetTemplateByNameAsync(string name);
        // Task<ApiResponse<object>> SendTemplateMessageAsync(string templateName, Dictionary<string, string> data);
    }
}
