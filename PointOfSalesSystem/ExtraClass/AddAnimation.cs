using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PointOfSalesSystem
{
    public class AddAnimation
    {
        public static void EditInfo(PictureBox picturebox, Label label, Image image, string info)
        {
            string defaultText = label.Text;

            picturebox.Image = image;
            label.Text = info;

            Timer animationTimer = new Timer();
            animationTimer.Interval = 1800;

            animationTimer.Tick += (sender, e) =>
            {
                animationTimer.Stop();

                picturebox.Image = Properties.Resources.edit_static;
                label.Text = defaultText;
            };

            animationTimer.Start();
        }
    }
}
