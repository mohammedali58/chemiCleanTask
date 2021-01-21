using System;
using System.Collections.Generic;

namespace ChemiClean.Core
{
    public class Product 
    {
        public int Id { get; set; }
        public DateTime? LastModified { get; set; }

        public string ProductName { get; set; }
        public string SupplierName { get; set; }

        public string Url { get; set; }
        public byte[] FileContent { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }

    }
}