using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    
    [Serializable]
    [DataContract]
    public partial class ResponsePage<T> where T : new()
    {
        [DataMember]
        [DisplayName("ResultCode")]
        public string ResultCode { get; set; }

        [DataMember]
        [DisplayName("Message")]
        public string Message { get; set; }

        [DataMember]
        [DisplayName("PageIndex")]
        public int PageIndex { get; set; }

        [DataMember]
        [DisplayName("PageSize")]
        public int PageSize { get; set; }

        [DataMember]
        [DisplayName("TotalRecords")]
        public long TotalRecords { get; set; }

        [DataMember]
        [DisplayName("Data")]
        public List<T> Data { get; set; }

        public ResponsePage()
        {
            ResultCode = string.Empty;
            Message = string.Empty;
            PageIndex = 0;
            PageSize = 0;
            TotalRecords = 0;
            Data = null;
        }
    }   
}
