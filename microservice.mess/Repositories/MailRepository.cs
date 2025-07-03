using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Configurations;
using microservice.mess.Interfaces;

namespace microservice.mess.Repositories
{
    public class MailRepository : IMailTemplate
    {
        private readonly IMongoCollection<MailTemplateModel> _templates;
        private readonly IMongoCollection<MailSenderAccountModel> _mailAccounts;
        private readonly ILogger<MailRepository> _logger;

        public MailRepository(IOptions<MongoSettings> mongoOptions, ILogger<MailRepository> logger, IMongoClient mongoClient)
        {
            _logger = logger;
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _templates = db.GetCollection<MailTemplateModel>("email_templates");
            _mailAccounts = db.GetCollection<MailSenderAccountModel>("email_accounts");
        }

        public async Task<MailSenderAccountModel?> GetByEmailAsync(string email)
        {
            return await _mailAccounts.Find(x => x.FromEmail == email).FirstOrDefaultAsync();
        }

        public async Task<List<MailSenderAccountModel>> GetAllAccountAsync()
        {
            return await _mailAccounts.Find(_ => true).ToListAsync();
        }

        public async Task CreateAccountAsync(MailSenderAccountModel model)
        {
            await _mailAccounts.InsertOneAsync(model);
        }

        public async Task CreateAsync(MailTemplateModel template)
        {
            template.CreatedAt = DateTime.UtcNow;
            await _templates.InsertOneAsync(template);
        }

        public async Task<List<MailTemplateModel>> GetAllAsync()
        {
            return await _templates.Find(_ => true).ToListAsync();
        }

        public async Task<MailTemplateModel?> GetByTagAsync(string tag)
        {
            return await _templates.Find(t => t.Tag == tag).FirstOrDefaultAsync();
        }
    }
}