using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem.DashboardMenu
{
    public partial class AdminMenu : Form
    {
        private readonly Dashboard dashboardFormRef;
        private readonly string username;
        private readonly string userRole;

        public AdminMenu(Dashboard dashboardFormRef, string username, string userRole)
        {
            InitializeComponent();

            this.dashboardFormRef = dashboardFormRef;
            this.username = username;
            this.userRole = userRole;
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.home_icon_white, new HomeForm(username));
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.home_icon_white, new HomeForm(username));

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnMenu, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.menu_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnDelivery, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.delivery_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHistory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.history_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnInventory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.inventory_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnManagement, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.manage_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnSettings, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.settings_icon_gray);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnMenu, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.menu_icon_white, new MenuForm(dashboardFormRef, username, userRole));

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.home_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnDelivery, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.delivery_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHistory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.history_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnInventory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.inventory_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnManagement, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.manage_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnSettings, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.settings_icon_gray);
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnDelivery, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.delivery_icon_white, new DeliverForm(dashboardFormRef, username));

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnMenu, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.menu_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.home_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHistory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.history_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnInventory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.inventory_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnManagement, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.manage_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnSettings, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.settings_icon_gray);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHistory, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.history_icon_white, new HistoryForm(username, userRole));

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnMenu, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.menu_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnDelivery, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.delivery_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.home_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnInventory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.inventory_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnManagement, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.manage_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnSettings, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.settings_icon_gray);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnInventory, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.inventory_icon_white, new InventoryForm(dashboardFormRef, this, username));

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnMenu, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.menu_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnDelivery, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.delivery_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHistory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.history_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.home_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnManagement, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.manage_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnSettings, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.settings_icon_gray);
        }

        private void btnManagement_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnManagement, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.manage_icon_white, new ManagementForm(username));

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnMenu, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.menu_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnDelivery, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.delivery_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHistory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.history_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnInventory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.inventory_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.home_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnSettings, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.settings_icon_gray);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnSettings, Color.FromArgb(176, 122, 50), Color.White, Properties.Resources.settings_icon_white, new SettingsForm());

            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnMenu, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.menu_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnDelivery, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.delivery_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHistory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.history_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnInventory, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.inventory_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnManagement, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.manage_icon_gray);
            FormUtilities.UpdateButton(dashboardFormRef.pnlMain, btnHome, Color.Transparent, Color.FromArgb(176, 164, 164), Properties.Resources.home_icon_gray);
        }
    }
}
