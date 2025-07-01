
using microservice.mess.Models;
using MongoDB.Bson;
using System.Text.Json;
using Newtonsoft.Json;

namespace microservice.mess.Extensions
{
    public static class ZaloButtonExtensions
    {
        public static BsonValue ToBsonPayload(this ZaloButton button)
        {
            if (button.Payload == null)
                return BsonNull.Value;

            // Trường hợp Payload là string
            if (button.Payload is string str)
            {
                str = str.Trim();

                // Nếu là JSON object hoặc array
                if ((str.StartsWith("{") && str.EndsWith("}")) ||
                    (str.StartsWith("[") && str.EndsWith("]")))
                {
                    try
                    {
                        // Thử parse nếu là JSON hợp lệ
                        return BsonDocument.Parse(str);
                    }
                    catch
                    {
                        // Nếu không parse được vẫn lưu string
                        return new BsonString(str);
                    }
                }

                // Không phải JSON → lưu dưới dạng string thường
                return new BsonString(str);
            }

            // Trường hợp JsonElement (từ System.Text.Json)
            if (button.Payload is JsonElement jsonElement)
            {
                try
                {
                    var rawJson = jsonElement.GetRawText();

                    // Kiểm tra xem là object hay array
                    if ((rawJson.StartsWith("{") && rawJson.EndsWith("}")) ||
                        (rawJson.StartsWith("[") && rawJson.EndsWith("]")))
                    {
                        return BsonDocument.Parse(rawJson);
                    }

                    return new BsonString(rawJson);
                }
                catch
                {
                    return new BsonString(jsonElement.ToString());
                }
            }

            // Trường hợp Dictionary
            if (button.Payload is Dictionary<string, string> dict)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(dict);
                    return BsonDocument.Parse(json);
                }
                catch
                {
                    return new BsonString(JsonConvert.SerializeObject(dict));
                }
            }

            // Fallback: chuyển sang string nếu không phải object
            return BsonValue.Create(button.Payload?.ToString());
        }

        public static void LoadFromBsonPayload(this ZaloButton button, BsonValue payload)
        {
            if (payload == null || payload.IsBsonNull)
                return;

            if (payload.IsString)
            {
                button.Payload = payload.AsString;
            }
            else if (payload.IsBsonDocument)
            {
                try
                {
                    button.Payload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload.ToJson());
                }
                catch
                {
                    button.Payload = payload.ToJson(); // fallback dạng JSON string
                }
            }
            else
            {
                button.Payload = payload.ToString();
            }
        }

    }
}
