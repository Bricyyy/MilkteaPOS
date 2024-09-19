using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PointOfSalesSystem.CustomObjects
{
    internal class CustomLabel : Label
    {
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            FitText();
        }

        private void FitText()
        {
            using (Graphics g = CreateGraphics())
            {
                string text = Text;
                SizeF textSize = g.MeasureString(text, Font, ClientSize.Width);

                if (textSize.Height > (Font.Height * 2)) // If the text exceeds two lines
                {
                    while (textSize.Height > (Font.Height * 2))
                    {
                        text = text.Substring(0, text.Length - 1);
                        textSize = g.MeasureString(text + "...", Font, ClientSize.Width);
                    }

                    Text = text + "..."; // Append three dots
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            FitText();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            FitText();
        }
    }
}
