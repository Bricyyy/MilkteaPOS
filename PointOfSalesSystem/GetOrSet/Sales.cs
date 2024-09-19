using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesSystem.GetOrSet
{
    public  class Sales
    {
        public int ReceiptId { get; set; }
        public int UserId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string ItemDesc { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal TotalChange { get; set; }
    }
}
