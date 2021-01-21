using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChemiClean.Core.DTOS 
{
    public class ProductUpdateDto : BaseDto
    {
        public DateTime? LastModified { get; set; }

        public string ProductName { get; set; }
        public string SupplierName { get; set; }

        public string Url { get; set; }
        [JsonIgnore]
        public byte[] FileContent { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }

    }
}
