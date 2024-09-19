using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesSystem
{
    public class Item
    {
        public int ItemId { get; set; }
        public byte[] ItemImage { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
        public decimal BasePrice { get; set; }
    }
}
