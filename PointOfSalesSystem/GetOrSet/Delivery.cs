using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesSystem.GetOrSet
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public string DeliveryNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ItemId { get; set; }
        public byte[] ItemImage { get; set; }
        public string ItemName { get; set; }
        public string ItemCategory { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}
