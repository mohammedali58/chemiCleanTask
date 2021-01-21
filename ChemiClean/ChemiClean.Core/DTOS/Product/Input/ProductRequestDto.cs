using ChemiClean.SharedKernel;
using System;
using System.Text.Json.Serialization;

namespace ChemiClean.Core.DTOS
{
    public class ProductRequestDto
    {
        public PagingModel Paging { get; set; }
        public SortingModel SortingModel { get; set; }

        public DateTime LastModified { get; set; }

        public string ProductName { get; set; }
        public string SupplierName { get; set; }

        public string Url { get; set; }
        [JsonIgnore]
        public byte[] FileContent { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }

    }
}
