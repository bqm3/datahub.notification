﻿using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class SYS_DATA_ZONE
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
        [SolrField("NAME")]
        [DisplayName("NAME")]
        public string NAME { get; set; }
        [DataMember]
        [SolrField("TYPE")]
        [DisplayName("TYPE")]
        public string TYPE { get; set; }
        [DataMember]
        [SolrField("IP")]
        [DisplayName("IP")]
        public string IP { get; set; }
        [DataMember]
        [SolrField("SERVICENAME")]
        [DisplayName("SERVICENAME")]
        public string SERVICENAME { get; set; }
        [DataMember]
        [SolrField("PORTNAME")]
        [DisplayName("PORTNAME")]
        public int? PORTNAME { get; set; }
        [DataMember]
        [SolrField("DBLINKNAME")]
        [DisplayName("DBLINKNAME")]
        public string DBLINKNAME { get; set; }
        [DataMember]
        [SolrField("NOTE")]
        [DisplayName("NOTE")]
        public string NOTE { get; set; }
        [DataMember]
        [SolrField("IS_ACTIVE")]
        [DisplayName("IS_ACTIVE")]
        public short? IS_ACTIVE { get; set; }
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
   
    public partial class SYS_DATA_ZONE_Request
    {
		[DataMember]
        [DisplayName("ID")]
        public long? ID { get; set; }
		[DataMember]		
		[DisplayName("CODE")]
		public string CODE { get; set; }

        [DataMember]
        [DisplayName("NAME")]
        public string NAME { get; set; }
        [DataMember]
        [DisplayName("TYPE")]
        public string TYPE { get; set; }
        [DataMember]
        [DisplayName("IP")]
        public string IP { get; set; }
        [DataMember]
        [DisplayName("SERVICENAME")]
        public string SERVICENAME { get; set; }
        [DataMember]
        [DisplayName("PORTNAME")]
        public int? PORTNAME { get; set; }
        [DataMember]
        [DisplayName("DBLINKNAME")]
        public string DBLINKNAME { get; set; }
        [DataMember]
        [DisplayName("NOTE")]
        public string NOTE { get; set; }
        [DataMember]
        [DisplayName("IS_ACTIVE")]
        public short? IS_ACTIVE { get; set; }
        [DataMember]
        [DisplayName("STATUS")]
        public short? STATUS { get; set; }		
		[DisplayName("REMOVED")]
		public short? REMOVED { get; set; }
        [DataMember]        
        [DisplayName("CREATOR")]
        public string CREATOR { get; set; }
        
    }

    public partial class SYS_DATA_ZONE_Search
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
