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

        public ZaloPromotionRepository(IMongoClient mongoClient, IOptions<MongoSettings> mongoOptions)
        {
            var settings = mongoOptions.Value;
            var db = mongoClient.GetDatabase(settings.ZaloDatabase);
            _promotionCollection = db.GetCollection<ZaloPromotionBson>("zalo_promotions");
        }

        private ZaloPromotionBson MapToBson(ZaloPromotionRequest model)
        {
            return new ZaloPromotionBson
            {
                Id = model.Id,
                Tag = model.Tag,
                Elements = model.Elements?.Select(e => new ZaloElementBson
                {
                    Type = e.Type,
                    Align = e.Align,
                    Content = e.Content,
                    AttachmentId = e.AttachmentId,
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
        private ZaloPromotionRequest MapToModel(ZaloPromotionBson bson)
        {
            return new ZaloPromotionRequest
            {
                Id = bson.Id,
                Tag = bson.Tag,
                Elements = bson.Elements?.Select(e => new ZaloElement
                {
                    Type = e.Type,
                    Align = e.Align,
                    Content = e.Content,
                    AttachmentId = e.AttachmentId,
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


        public async Task<ZaloPromotionRequest?> GetPromotionByIdAsync(string id)
        {
            var bson = await _promotionCollection.Find(x => x.Tag == id).FirstOrDefaultAsync();
            return bson == null ? null : MapToModel(bson);
        }

        public async Task InsertOneAsync(ZaloPromotionRequest promotion)
        {
            var bson = MapToBson(promotion);
            await _promotionCollection.ReplaceOneAsync(
                filter: Builders<ZaloPromotionBson>.Filter.Eq(x => x.Id, bson.Id),
                replacement: bson,
                options: new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task<bool> UpdatePromotionAsync(string id, ZaloPromotionRequest updatedPromotion)
        {
            var bson = MapToBson(updatedPromotion);
            var result = await _promotionCollection.ReplaceOneAsync(x => x.Id == id, bson);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeletePromotionAsync(string id)
        {
            var result = await _promotionCollection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<ZaloPromotionRequest>> GetAllAsync()
        {
            var all = await _promotionCollection.Find(_ => true).ToListAsync();
            return all.Select((ZaloPromotionBson x) => MapToModel(x)).ToList();
        }

    }
}
