using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesSystem.GetOrSet
{
    public class OrderSummary
    {
        public int CartId { get; set; }
        public int InventoryId { get; set; }
        public int ItemAvailable { get; set; }
        public int ItemId { get; set; }
        public byte[] ItemImage { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string Size { get; set; }
        public string Sugar { get; set; }
        public string Ice { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }
    }
}
