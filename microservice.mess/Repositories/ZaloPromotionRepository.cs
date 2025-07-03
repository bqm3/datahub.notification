using Microsoft.Extensions.Options;
using MongoDB.Driver;
using microservice.mess.Models;
using microservice.mess.Configurations;
using microservice.mess.Extensions;

namespace microservice.mess.Repositories
{
    public class ZaloPromotionRepository
    {
        private readonly IMongoCollection<ZaloPromotionBson> _promotionCollection;
        private readonly ILogger<ZaloPromotionRepository> _logger;
        public ZaloPromotionRepository(IMongoClient mongoClient, IOptions<MongoSettings> mongoOptions, ILogger<ZaloPromotionRepository> logger) 
        {
            _logger = logger;
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _promotionCollection = db.GetCollection<ZaloPromotionBson>("zalo_promotions");
        }

        public ZaloPromotionBson MapToBson(ZaloPromotionRequest model, string? existingId = null)
        {
            return new ZaloPromotionBson
            {
                Id = existingId ?? model.Id,
                Tag = model.Tag,
                Elements = model.Elements?.Select(e => new ZaloElementBson
                {
                    Type = e.Type,
                    AttachmentId = e.AttachmentId,
                    Align = e.Align,
                    Content = e.Content,
                    ContentTable = e.ContentTable?.Select(ct => new ZaloTableContent
                    {
                        Key = ct.Key,
                        Value = ct.Value
                    }).ToList()
                }).ToList(),
                Buttons = model.Buttons?.Select(b => new ZaloButtonBson
                {
                    Title = b.Title,
                    ImageIcon = b.ImageIcon,
                    Type = b.Type,
                    Payload = b.ToBsonPayload()
                }).ToList()
            };
        }
        public ZaloPromotionRequest MapToModel(ZaloPromotionBson bson)
        {
            return new ZaloPromotionRequest
            {
                Id = bson.Id,
                Tag = bson.Tag,
                Elements = bson.Elements?.Select(e => new ZaloElement
                {
                    Type = e.Type,
                    AttachmentId = e.AttachmentId,
                    Align = e.Align,
                    Content = e.Content,
                    ContentTable = e.ContentTable?.Select(ct => new ZaloTableRow
                    {
                        Key = ct.Key,
                        Value = ct.Value
                    }).ToList()
                }).ToList(),
                Buttons = bson.Buttons?.Select(b =>
                {
                    var btn = new ZaloButton
                    {
                        Title = b.Title,
                        ImageIcon = b.ImageIcon,
                        Type = b.Type
                    };
                    btn.LoadFromBsonPayload(b.Payload);
                    return btn;
                }).ToList()
            };
        }

        public async Task<bool> TagExistsAsync(string tag)
        {
            var count = await _promotionCollection.CountDocumentsAsync(x => x.Tag == tag);
            return count > 0;
        }

        public async Task<ZaloPromotionRequest?> GetPromotionByTagAsync(string tag)
        {
            var bson = await _promotionCollection.Find(x => x.Tag == tag).FirstOrDefaultAsync();
            return bson == null ? null : MapToModel(bson);
        }

        public async Task InsertOrUpdateByTagAsync(ZaloPromotionRequest promotion)
        {
            var existing = await _promotionCollection.Find(x => x.Tag == promotion.Tag).FirstOrDefaultAsync();

            var bson = MapToBson(promotion, existing?.Id);

            await _promotionCollection.ReplaceOneAsync(
                filter: Builders<ZaloPromotionBson>.Filter.Eq(x => x.Tag, bson.Tag),
                replacement: bson,
                options: new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task<bool> UpdatePromotionByTagAsync(string tag, ZaloPromotionRequest updatedPromotion)
        {
            var existing = await _promotionCollection.Find(x => x.Tag == tag).FirstOrDefaultAsync();
            if (existing == null) return false;

            var bson = MapToBson(updatedPromotion, existing.Id); // giữ nguyên _id cũ

            var result = await _promotionCollection.ReplaceOneAsync(x => x.Tag == tag, bson);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeletePromotionByTagAsync(string tag)
        {
            var result = await _promotionCollection.DeleteOneAsync(x => x.Tag == tag);
            return result.DeletedCount > 0;
        }

        public async Task<List<ZaloPromotionRequest>> GetAllAsync()
        {
            var all = await _promotionCollection.Find(_ => true).ToListAsync();
            return all.Select((ZaloPromotionBson x) => MapToModel(x)).ToList();
        }

    }
}
