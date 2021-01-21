using ChemiClean.SharedKernel;
using System;
using System.Text.Json.Serialization;

namespace ChemiClean.Core.DTOS
{
    public class ProductResponseDto : BaseDto
    {
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string Url { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime? LastModified { get; set; }
        public byte[] FileContent { get; set; }

    }
}
