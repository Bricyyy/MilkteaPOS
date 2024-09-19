using PointOfSalesSystem.GetOrSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PointOfSalesSystem.SalesForms
{
    public partial class OrderHistoryLists : Form
    {
        HistoryForm historyFormRef;

        public OrderHistoryLists(HistoryForm historyFormRef)
        {
            InitializeComponent();

            this.historyFormRef = historyFormRef;
        }

        private Guna.UI2.WinForms.Guna2Panel GenerateHistoryPanel(string receiptNo, int itemCount, string itemsInfo, string totalAmount, string orderDate, string orderTime)
        {
            pnlHistory = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = Color.White,
                Name = "pnlHistory",
                Size = new Size(936, 69)
            };

            lblReceiptNo = new System.Windows.Forms.Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = "#" + receiptNo,
                AutoEllipsis = true,
                Name = "lblReceiptNo",
                AutoSize = true,
                Location = new Point(19, 28),
                MaximumSize = new Size(105, 16)
            };

            lblItemCount = new System.Windows.Forms.Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = itemCount + " item/s",
                AutoEllipsis = true,
                Name = "lblItemCount",
                AutoSize = true,
                Location = new Point(173, 28),
                MaximumSize = new Size(74, 16)
            };

            lblItemInfo = new System.Windows.Forms.Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = itemsInfo,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
                Name = "lblItemInfo",
                AutoSize = true,
                Location = new Point(303, 9),
                MinimumSize = new Size(166, 50),
                MaximumSize = new Size(166, 50)
            };

            lblTotalAmount = new System.Windows.Forms.Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = "₱" + totalAmount,
                AutoEllipsis = true,
                Name = "lblTotalAmount",
                AutoSize = true,
                Location = new Point(512, 28),
                MaximumSize = new Size(109, 16)
            };

            lblOrderDate = new System.Windows.Forms.Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = orderDate,
                AutoEllipsis = true,
                Name = "lblOrderDate",
                AutoSize = true,
                Location = new Point(654, 18),
                MaximumSize = new Size(121, 16)
            };

            lblOrderTime = new System.Windows.Forms.Label
            {
                BackColor = Color.Transparent,
                Font = new Font("Proxima Nova Lt", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Text = orderTime,
                AutoEllipsis = true,
                Name = "lblOrderTime",
                AutoSize = true,
                Location = new Point(654, 34),
                MaximumSize = new Size(109, 16)
            };

            btnOrderDetails = new Guna.UI2.WinForms.Guna2Button
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Font = new Font("Proxima Nova Lt", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 57, 50),
                Name = "btnOrderDetails",
                Location = new Point(826, 17),
                Size = new Size(93, 35),
                Animated = true,
                BorderColor = Color.FromArgb(64, 184, 76),
                BorderRadius = 15,
                BorderThickness = 1,
                FillColor = Color.Transparent,
                UseTransparentBackground = true,
                Text = "Order Details",
                Tag = receiptNo
            };

            pnlHistory.Controls.Add(lblReceiptNo);
            pnlHistory.Controls.Add(lblItemCount);
            pnlHistory.Controls.Add(lblItemInfo);
            pnlHistory.Controls.Add(lblTotalAmount);
            pnlHistory.Controls.Add(lblOrderDate);
            pnlHistory.Controls.Add(lblOrderTime);
            pnlHistory.Controls.Add(btnOrderDetails);

            btnOrderDetails.Click += BtnOrderDetails_Click;

            return pnlHistory;
        }

        private void CopyAndPasteHistoryPanel(string receiptNo, int itemCount, string itemsInfo, string totalAmount, DateTime orderDateTime)
        {
            string formattedDate = orderDateTime.ToString("MMMM dd, yyyy");
            string formattedTime = orderDateTime.ToString("hh:mmtt").ToUpper();

            Guna.UI2.WinForms.Guna2Panel clonedPanel = GenerateHistoryPanel(receiptNo, itemCount, itemsInfo, totalAmount, formattedDate, formattedTime);
            flpHistories.Controls.Add(clonedPanel);
        }

        private string BuildHistoryQuery(DateTime startDate, DateTime endDate, string searchText)
        {
            string formattedStartDate = startDate.ToString("yyyy-MM-dd");
            string formattedEndDate = endDate.ToString("yyyy-MM-dd");

            string historyQuery;
            if (!string.IsNullOrEmpty(searchText))
            {
                historyQuery = $@"SELECT Receipt_Id, User_Id, Receipt_No, Receipt_Date, Item_Id, Quantity, Item_Desc, Total_Amount, Amount_Paid, Total_Change
                       FROM sales
                       WHERE Receipt_No LIKE '%{searchText}%' AND Receipt_Date >= '{formattedStartDate} 00:00:00' AND Receipt_Date <= '{formattedEndDate} 23:59:59'
                       ORDER BY Receipt_Id DESC;";
            }
            else
            {
                historyQuery = $@"SELECT Receipt_Id, User_Id, Receipt_No, Receipt_Date, Item_Id, Quantity, Item_Desc, Total_Amount, Amount_Paid, Total_Change
                       FROM sales
                       WHERE Receipt_Date >= '{formattedStartDate} 00:00:00' AND Receipt_Date <= '{formattedEndDate} 23:59:59'
                       ORDER BY Receipt_Id DESC;";
            }

            return historyQuery;
        }

        private void SetHistory()
        {
            DateTime startDate = historyFormRef.dtpFirstRange.Value.Date;
            DateTime endDate = historyFormRef.dtpSecondRange.Value.Date;
            string searchText = historyFormRef.txtSearch.Text;

            string salesQuery = BuildHistoryQuery(startDate, endDate.Date, searchText);

            List<Sales> sales = DataAccess.GetSales(salesQuery);

            HashSet<string> uniqueReceiptNumbers = new HashSet<string>();

            decimal totalSales = 0;

            foreach (Sales sale in sales)
            {
                uniqueReceiptNumbers.Add(sale.ReceiptNo);
            }

            foreach (string receiptNo in uniqueReceiptNumbers)
            {
                int itemCount = sales.Count(s => s.ReceiptNo == receiptNo);

                // Get distinct item IDs associated with the current receipt number
                var distinctItemIds = sales.Where(s => s.ReceiptNo == receiptNo).Select(s => s.ItemId).Distinct().ToList();

                List<string> itemNames = new List<string>();

                // Fetch item names for each distinct item ID
                foreach (int itemId in distinctItemIds)
                {
                    string itemQuery = $"SELECT * FROM items WHERE Item_Id = {itemId}";
                    List<Item> items = DataAccess.GetItems(itemQuery);

                    if (items.Count > 0)
                    {
                        itemNames.Add(items[0].ItemName); // Assuming only one item is fetched for an item ID
                    }
                }

                string itemsInfo = string.Join(", ", itemNames);

                // Get the first sale entry for the receipt number to retrieve the total amount
                Sales firstSale = sales.FirstOrDefault(s => s.ReceiptNo == receiptNo);

                if (firstSale != null)
                {
                    CopyAndPasteHistoryPanel(receiptNo, itemCount, itemsInfo, firstSale.TotalAmount.ToString(), firstSale.ReceiptDate);

                    totalSales += firstSale.TotalAmount;
                }
            }

            historyFormRef.lblTotalSales.Text = "₱" + totalSales.ToString();
            historyFormRef.lblTotalTransactions.Text = uniqueReceiptNumbers.Count.ToString();
        }

        private void OrderHistoryLists_Load(object sender, EventArgs e)
        {
            flpHistories.Controls.Clear();
            SetHistory();
        }

        private void BtnOrderDetails_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna.UI2.WinForms.Guna2Button btnDetails)) return;

            string receiptNo = btnDetails.Tag?.ToString();

            historyFormRef.loadForm(new OrderDetails(historyFormRef, receiptNo));
        }

        private Guna.UI2.WinForms.Guna2Panel pnlHistory = new Guna.UI2.WinForms.Guna2Panel();
        private System.Windows.Forms.Label lblReceiptNo = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblItemCount = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblItemInfo = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblTotalAmount = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblOrderDate = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblOrderTime = new System.Windows.Forms.Label();
        private Guna.UI2.WinForms.Guna2Button btnOrderDetails = new Guna.UI2.WinForms.Guna2Button();
    }
}
