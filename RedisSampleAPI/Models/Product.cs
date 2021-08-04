using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisSampleAPI.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}
