using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSalesSystem.ExtraClass
{
    public class StepIndicator
    {
        private readonly PictureBox[] stepPictures;
        private readonly Label[] stepLabels;

        private readonly Color defaultColor = Color.FromArgb(193, 200, 207);
        private readonly Color selectedColor = Color.FromArgb(176, 122, 50);

        public StepIndicator(PictureBox[] pictures, Label[] labels)
        {
            if (pictures.Length != labels.Length)
                throw new ArgumentException("The number of pictures and labels must be the same.");

            stepPictures = pictures;
            stepLabels = labels;

            // Set the default image and color for all steps
            SetDefaultState();
        }

        public void SetStep(int currentStep)
        {
            if (currentStep < 1 || currentStep > stepPictures.Length)
                throw new ArgumentOutOfRangeException("currentStep", "Invalid step number");

            // Set the default image and color for all steps
            SetDefaultState();

            // Set the selected image for the current step
            string imageName = $"number{currentStep}_colored";
            stepPictures[currentStep - 1].Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
            stepLabels[currentStep - 1].ForeColor = selectedColor;
        }

        private void SetDefaultState()
        {
            // Set the default image and color for all steps
            for (int i = 0; i < stepPictures.Length; i++)
            {
                string imageName = $"number{i + 1}_normal";
                stepPictures[i].Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
                stepLabels[i].ForeColor = defaultColor;
            }
        }
    }
}
