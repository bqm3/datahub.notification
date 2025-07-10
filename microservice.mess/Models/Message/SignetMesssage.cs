using microservice.mess.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace microservice.mess.Models.Message
{

    public class SGIActionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Type { get; set; } = default!;
        public string Value { get; set; } = default!;
        public string? Message { get; set; }
        public Dictionary<string, object>? Parameter { get; set; }
        public Dictionary<string, object>? Options { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SGIUploadFileHashModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string FileName { get; set; } = default!;
        public string FileHash { get; set; } = default!;
        public string FileType { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }


    public class SgiMessageTemplate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("skip_receiver_error")]
        public bool Skip_Receiver_Error { get; set; }

        [BsonElement("receivers")]
        public List<string> Receivers { get; set; } = new();

        [BsonElement("block_json")]
        public string BlockJson { get; set; } = "{}";
        public DateTime CreatedAt { get; set; }
    }

    public class SgiDataPdfTemplate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // ánh xạ với MongoDB _id
        [BsonElement("code")]
        public string Code { get; set; }
        [BsonElement("services")]
        public string Services { get; set; }
        [BsonElement("topic")]
        public string Topic { get; set; }
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }
        [BsonElement("message")]
        public MessageContent Message { get; set; }
    }

    public class MessageContent
    {
        [BsonElement("properties")]
        public Dictionary<string, List<string>> Properties { get; set; }
        [BsonElement("entity")]
        public string Entity { get; set; }
        [BsonElement("chuyen_de")]
        public string ChuyenDe { get; set; }
    }

    public class SgiMessageTemplateDto
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Receivers { get; set; } = new();
        public bool Skip_Receiver_Error { get; set; }

        // public string MessageType { get; set; } = "normal";
        // public int Version { get; set; } = 3;
        public object Block { get; set; }
    }


    public class SendTemplateMessageRequest
    {
        public string TemplateName { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public List<string>? Receivers { get; set; }
    }

    public class CallbackRequest
    {
        public string CallbackUrl { get; set; }
    }

    public class SignetMessageRequest
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("receivers")]
        public List<string> Receivers { get; set; } = new();
        [BsonElement("block")]
        public SignetMessage Block { get; set; } = new();
        [BsonElement("skip_receiver_error")]
        public bool? Skip_Receiver_Error { get; set; } = new();
    }

    public class SignetMessage
    {
        [BsonElement("layout")]
        public string? Layout { get; set; }
        [BsonElement("message_type")]
        public string MessageType { get; set; }
        [BsonElement("version")]
        public int Version { get; set; }
        [BsonElement("data")]
        public List<SignetBody>? Data { get; set; } = new();
        [BsonElement("data_modal")]
        public SignetBodyModal? DataModal { get; set; } = new();
    }

    public class SignetBody
    {
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("data")]
        public SignetBodyItem? Data { get; set; }
    }

    public class SignetBodyModal
    {
        [BsonElement("title")]
        public SignetComponent? Title { get; set; }
        [BsonElement("blocks")]
        public List<SignetComponent>? Blocks { get; set; }
        [BsonElement("close")]
        public ModalButton? Close { get; set; }
        [BsonElement("submit")]
        public ModalSubmit? Submit { get; set; }
        public ModalPagination? Pagination { get; set; }
    }

    public class InputComponentData
    {
        public string Type { get; set; }
        public string FieldName { get; set; }
        public InputOptions Options { get; set; } = new();
    }

    public class InputOptions
    {
        [BsonElement("required")]
        public bool Required { get; set; }
        [BsonElement("is_hidden")]
        public bool IsHidden { get; set; }
        [BsonElement("placeholder")]
        public string Placeholder { get; set; }
        [BsonElement("value")]
        public object Value { get; set; }
    }

    public class SignetComponent

    {
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("data")]
        public SignetBodyFieldItem? Data { get; set; }
        [BsonElement("label")]
        public SignetBodyItem? Label { get; set; }
    }

    public class SignetUserComponent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Role { get; set; }
        public string? Group { get; set; }
    }

    public class SignetBodyFieldItem
    {
        [BsonElement("field_name")]
        public string? FieldName { get; set; }
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("options")]
        public InputOptions? Options { get; set; }
    }

    public class ModalButton
    {
        public string Text { get; set; }
        public bool IsHidden { get; set; }
    }

    public class ModalSubmit
    {
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("is_hidden")]
        public bool IsHidden { get; set; }
        [BsonElement("cmd_value")]
        public string CmdValue { get; set; }
        [BsonElement("parameter")]
        public Dictionary<string, object>? Parameter { get; set; }
    }

    public class ModalPagination

    {
        public int TotalPage { get; set; }
        public string Text { get; set; }
        public int CurrentPage { get; set; }
        public PaginationAction Action { get; set; }
    }

    public class PaginationAction
    {
        public string Type { get; set; } = "send_cmd";
        public string Value { get; set; } = "reload_modal_data";
        public PaginationParameter Parameter { get; set; } = new();
    }

    public class PaginationParameter
    {
        public string MsgId { get; set; }
        public int Page { get; set; }
    }

    public class SignetBodyItem
    {
        [BsonElement("style")]
        public string? Style { get; set; }
        [BsonElement("text")]
        public string? Text { get; set; }
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("url")]
        public string? Url { get; set; }
        public SignetBodyItemAction? Action { get; set; }

    }

    public class SignetBodyItemAction
    {
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("value")]
        public string? Value { get; set; }
        [BsonElement("message")]
        public string? Message { get; set; }
    }
}
