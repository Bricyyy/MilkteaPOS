using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesSystem.GetOrSet
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Sugar { get; set; }
        public string Ice { get; set; }
    }
}
