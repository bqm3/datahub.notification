using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Models.Message;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Repositories
{
    public class MailRepository : IMailTemplate
    {
        private readonly IMongoCollection<AllMessageTemplate> _templates;
        private readonly IMongoCollection<UserAccountModel> _mailAccounts;
        private readonly ILogger<MailRepository> _logger;

        public MailRepository(IOptions<MongoSettings> mongoOptions, ILogger<MailRepository> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _templates = db.GetCollection<AllMessageTemplate>("all_templates");
            _mailAccounts = db.GetCollection<UserAccountModel>("all_accounts");
        }

        public async Task<UserAccountModel?> GetByEmailAsync(string email)
        {
            return await _mailAccounts.Find(x => x.FromEmail == email).FirstOrDefaultAsync();
        }

        public async Task<List<UserAccountModel>> GetAllAccountAsync()
        {
            return await _mailAccounts.Find(_ => true).ToListAsync();
        }

        public async Task CreateAccountAsync(UserAccountModel model)
        {
            await _mailAccounts.InsertOneAsync(model);
        }

        public async Task CreateAsync(AllMessageTemplate template)
        {
            template.CreatedAt = DateTime.UtcNow;
            await _templates.InsertOneAsync(template);
        }

        public async Task<List<AllMessageTemplate>> GetAllAsync()
        {
            return await _templates.Find(_ => true).ToListAsync();
        }

        public async Task<AllMessageTemplate?> GetByNameAsync(string name)
        {
            return await _templates.Find(t => t.Name == name).FirstOrDefaultAsync();
        }
    }
}