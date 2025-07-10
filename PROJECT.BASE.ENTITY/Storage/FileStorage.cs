using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class MinioModel
    {
        [DataMember]        
        [DisplayName("FolderName")]
        public string FolderName { get; set; }
        [DataMember]
        [DisplayName("OriginalFileName")]
        public string OriginalFileName { get; set; }
        [DataMember]
        [DisplayName("FileName")]
        public string FileName { get; set; }
        [DataMember]
        [DisplayName("FileSize")]
        public long? FileSize { get; set; }
        [DataMember]
        [DisplayName("ContentType")]
        public string ContentType { get; set; }
        [DataMember]
        [DisplayName("PublicUrl")]
        public string PublicUrl { get; set; }
    }
    public partial class MinioFileInfo
    {
        [DataMember]
        [DisplayName("Code")]
        public string Code { get; set; }
        [DataMember]
        [DisplayName("FileName")]
        public string FileName { get; set; }
        [DataMember]
        [DisplayName("FileSize")]
        public long? FileSize { get; set; }
        public DateTime? LastModified { get; set; }
    }
}

