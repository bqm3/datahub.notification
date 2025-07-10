using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class ITG_MSGI
    {
        [DataMember]
        [SolrUniqueKey("ID")]
        [DisplayName("ID")]
        public decimal? ID { get; set; }
        [DataMember]
        [SolrField("CODE")]
        [DisplayName("CODE")]
        public string CODE { get; set; }

        [DataMember]
        [SolrField("MSG_ID")]
        [DisplayName("MSG_ID")]
        public string MSG_ID { get; set; }
        [DataMember]
        [SolrField("MSG_REFID")]
        [DisplayName("MSG_REFID")]
        public string MSG_REFID { get; set; }
        [DataMember]
        [SolrField("MSG_CONTENT")]
        [DisplayName("MSG_CONTENT")]
        public string MSG_CONTENT { get; set; }
        [DataMember]
        [SolrField("TRAN_CODE")]
        [DisplayName("TRAN_CODE")]
        public string TRAN_CODE { get; set; }
        [DataMember]
        [SolrField("TRAN_NAME")]
        [DisplayName("TRAN_NAME")]
        public string TRAN_NAME { get; set; }
        [DataMember]
        [SolrField("SENDER_CODE")]
        [DisplayName("SENDER_CODE")]
        public string SENDER_CODE { get; set; }
        [DataMember]
        [SolrField("SENDER_NAME")]
        [DisplayName("SENDER_NAME")]
        public string SENDER_NAME { get; set; }
        [DataMember]
        [SolrField("RECEIVER_CODE")]
        [DisplayName("RECEIVER_CODE")]
        public string RECEIVER_CODE { get; set; }
        [DataMember]
        [SolrField("RECEIVER_NAME")]
        [DisplayName("RECEIVER_NAME")]
        public string RECEIVER_NAME { get; set; }
        [DataMember]
        [SolrField("DATA_TYPE")]
        [DisplayName("DATA_TYPE")]
        public string DATA_TYPE { get; set; }
        [DataMember]
        [SolrField("SEND_DATE")]
        [DisplayName("SEND_DATE")]
        public DateTime? SEND_DATE { get; set; }
        [DataMember]
        [SolrField("ORIGINAL_CODE")]
        [DisplayName("ORIGINAL_CODE")]
        public string ORIGINAL_CODE { get; set; }
        [DataMember]
        [SolrField("ORIGINAL_NAME")]
        [DisplayName("ORIGINAL_NAME")]
        public string ORIGINAL_NAME { get; set; }
        [DataMember]
        [SolrField("ORIGINAL_DATE")]
        [DisplayName("ORIGINAL_DATE")]
        public DateTime? ORIGINAL_DATE { get; set; }
        [DataMember]
        [SolrField("VERSION")]
        [DisplayName("VERSION")]
        public string VERSION { get; set; }
        [DataMember]
        [SolrField("STATUS")]
        [DisplayName("STATUS")]
        public short? STATUS { get; set; }     
		[DataMember]
		[SolrField("IS_DELETE")]
		[DisplayName("IS_DELETE")]
		public short? IS_DELETE { get; set; }
        [DataMember]
        [SolrField("CDATE")]
        [DisplayName("CDATE")]
        public DateTime? CDATE { get; set; }
        [DataMember]
        [SolrField("LDATE")]
        [DisplayName("LDATE")]
        public DateTime? LDATE { get; set; }
        [DataMember]
        [SolrField("CUSER")]
        [DisplayName("CUSER")]
        public string CUSER { get; set; }
        [DataMember]
        [SolrField("LUSER")]
        [DisplayName("LUSER")]
        public string LUSER { get; set; }
       
    }    
    public partial class ITG_MSGI_Request
    {
        [DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public ITG_MSGI_Information Information { get; set; }

    }
    public partial class ITG_MSGI_Information
    {
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("MSG_ID")]
        public string MSG_ID { get; set; }
        [DataMember]
        [DisplayName("MSG_REFID")]
        public string MSG_REFID { get; set; }
        [DataMember]
        [DisplayName("MSG_CONTENT")]
        public string MSG_CONTENT { get; set; }
        [DataMember]
        [DisplayName("TRAN_CODE")]
        public string TRAN_CODE { get; set; }
        [DataMember]
        [DisplayName("TRAN_NAME")]
        public string TRAN_NAME { get; set; }
        [DataMember]
        [DisplayName("SENDER_CODE")]
        public string SENDER_CODE { get; set; }
        [DataMember]
        [DisplayName("SENDER_NAME")]
        public string SENDER_NAME { get; set; }
        [DataMember]
        [DisplayName("RECEIVER_CODE")]
        public string RECEIVER_CODE { get; set; }
        [DataMember]
        [DisplayName("RECEIVER_NAME")]
        public string RECEIVER_NAME { get; set; }
        [DataMember]
        [DisplayName("DATA_TYPE")]
        public string DATA_TYPE { get; set; }
        [DataMember]
        [DisplayName("SEND_DATE")]
        public string SEND_DATE { get; set; }
        [DataMember]
        [DisplayName("ORIGINAL_CODE")]
        public string ORIGINAL_CODE { get; set; }
        [DataMember]
        [DisplayName("ORIGINAL_NAME")]
        public string ORIGINAL_NAME { get; set; }
        [DataMember]
        [DisplayName("ORIGINAL_DATE")]
        public string ORIGINAL_DATE { get; set; }
        [DataMember]
        [DisplayName("VERSION")]
        public string VERSION { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public short? STATUS { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class ITG_MSGI_Search
    {
        
        [DataMember]        
        [DisplayName("SearchField")]
        public Dictionary<string,object> SearchField { get; set; }
        [DataMember]        
        [DisplayName("CDATE_START")]
        public string CDATE_START { get; set; }
        [DataMember]        
        [DisplayName("CDATE_END")]
        public string CDATE_END { get; set; }
        [DataMember]        
        [DisplayName("PageIndex")]
        public int? PageIndex { get; set; }
        [DataMember]        
        [DisplayName("PageSize")]
        public int? PageSize { get; set; }

    }

}
