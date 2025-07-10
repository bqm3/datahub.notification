using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json;

namespace PROJECT.BASE.ENTITY
{
    public class KafkaMessage
    {
        public string TopicName { get; set; }
        //public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public object? Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    public class KafkaRequest
    {
        public string SenderCode { get; set; }
        public string MessageType { get; set; }
        //format yyyy-MM-dd HH:mm:ss
        public string SendDate { get; set; } 
        public object? Data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    //EventStreams
    public class EventStreamInfo
    {
        public string Id { get; set; }
        public string Timestamp { get; set; }
        public IntegrationRequest Data { get; set; }      

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    #region
    public class HeaderInfo
    {
        public string Version { get; set; }
        public string Sender_Code { get; set; }
        public string Sender_Name { get; set; }
        public string Tran_Code { get; set; }
        public string Tran_Name { get; set; }
        public string Receiver_Code { get; set; }
        public string Receiver_Name { get; set; }
        public string Request_Id { get; set; }
        public string Msg_Id { get; set; }
        public string Msg_Refid { get; set; }
        public string Data_Type { get; set; }
        //format yyyy-MM-dd HH:mm:ss
        public string Send_Date { get; set; }
        public string Original_Code { get; set; }
        public string Original_Name { get; set; }
        //format yyyy-MM-dd HH:mm:ss
        public string Original_Date { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    public class BodyInfo
    {
        public string Content { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    public class SecurityInfo
    {
        public string Signature { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    #endregion
    public class IntegrationRequest
    {
        public HeaderInfo Header { get; set; }
        public BodyInfo Body { get; set; }
        public SecurityInfo Security { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    public class LogRequest
    {
        public string LogType { get; set; }
        public Dictionary<string,object> Information { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    
}
