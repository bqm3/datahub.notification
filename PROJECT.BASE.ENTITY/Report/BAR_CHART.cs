using MongoDB.Bson;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PROJECT.BASE.ENTITY
{
    public partial class BAR_CHART
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }

        [DataMember]
        [DisplayName("SYSTEM_CODE")]
        public string SYSTEM_CODE { get; set; }
        [DataMember]
        [DisplayName("SERVICE_CODE")]
        public string SERVICE_CODE { get; set; }
        [DataMember]
        [DisplayName("SERVICE_NAME")]
        public string SERVICE_NAME { get; set; }
        [DataMember]
        [DisplayName("SERVICE_TYPE")]
        public string SERVICE_TYPE { get; set; }
        [DataMember]
        [DisplayName("COLOR_CODE")]
        public string COLOR_CODE { get; set; }
        [DataMember]
        [DisplayName("COLOR_CLASS")]
        public string COLOR_CLASS { get; set; }
        [DataMember]
        [DisplayName("REPORT_TYPE")]
        public string REPORT_TYPE { get; set; }
        [DataMember]
        [DisplayName("VALUES")]
        public decimal? VALUES { get; set; }
        [DataMember]
        [DisplayName("DATE")]
        public long? DATE { get; set; }      
        [DataMember]
        [DisplayName("ISDELETE")]
        public bool? ISDELETE { get; set; }
        [DataMember]
        [DisplayName("CUSER")]
        public string CUSER { get; set; }
        [DataMember]
        [DisplayName("CDATE")]
        public long? CDATE { get; set; }
        [DataMember]
        [DisplayName("LUSER")]
        public string LUSER { get; set; }
        [DataMember]
        [DisplayName("LDATE")]
        public long? LDATE { get; set; }

    }

    public partial class BAR_CHART_Request
    {
        [DataMember]
        [DisplayName("CODE")]
        public string CODE { get; set; }
        [DataMember]
        [DisplayName("Information")]
        public BAR_CHART_Information Information { get; set; }

    }
    public partial class BAR_CHART_Information
    {        

        [DataMember]
        [DisplayName("SYSTEM_CODE")]
        public string SYSTEM_CODE { get; set; }
        [DataMember]
        [DisplayName("SERVICE_CODE")]
        public string SERVICE_CODE { get; set; }
        [DataMember]
        [DisplayName("SERVICE_NAME")]
        public string SERVICE_NAME { get; set; }
        [DataMember]
        [DisplayName("SERVICE_TYPE")]
        public string SERVICE_TYPE { get; set; }
        [DataMember]
        [DisplayName("COLOR_CODE")]
        public string COLOR_CODE { get; set; }
        [DataMember]
        [DisplayName("COLOR_CLASS")]
        public string COLOR_CLASS { get; set; }
        [DataMember]
        [DisplayName("REPORT_TYPE")]
        public string REPORT_TYPE { get; set; }
        [DataMember]
        [DisplayName("VALUES")]
        public long? VALUES { get; set; }
        [DataMember]
        [DisplayName("DATE")]
        public long? DATE { get; set; }
		[DataMember]
        [DisplayName("ISDELETE")]
        public bool? ISDELETE { get; set; }

    }

    public partial class BAR_CHART_SearchRequest
    {

        [DataMember]
        [DisplayName("SYSTEM_CODE")]
        public string SYSTEM_CODE { get; set; }
        [DataMember]
        [DisplayName("SERVICE_CODE")]
        public string SERVICE_CODE { get; set; }
        [DataMember]
        [DisplayName("SERVICE_NAME")]
        public string SERVICE_NAME { get; set; }
        [DataMember]
        [DisplayName("SERVICE_TYPE")]
        public string SERVICE_TYPE { get; set; }
        [DataMember]
        [DisplayName("REPORT_TYPE")]
        public string REPORT_TYPE { get; set; }
        [DataMember]
        [DisplayName("VALUES_Start")]
        public long? VALUES_Start { get; set; }
        [DataMember]
        [DisplayName("VALUES_End")]
        public long? VALUES_End { get; set; }
        [DataMember]
        [DisplayName("DATE_Start")]
        public long? DATE_Start { get; set; }
        [DataMember]
        [DisplayName("DATE_End")]
        public long? DATE_End { get; set; }
        [DataMember]
        [DisplayName("ISDELETE")]
        public bool? ISDELETE { get; set; }
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
