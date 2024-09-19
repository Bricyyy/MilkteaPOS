using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageMagick;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace PointOfSalesSystem
{
    public class GifSplitter
    {
        public void SplitGifIntoFrames(string gifFilePath, string outputFolderPath)
        {
            using (MagickImageCollection collection = new MagickImageCollection(gifFilePath))
            {
                int frameNumber = 0;
                foreach (MagickImage image in collection)
                {
                    image.Write(Path.Combine(outputFolderPath, $"frame_{frameNumber}.png"));
                    frameNumber++;
                }
            }
        }

        public static List<Image> LoadGifFrames(Image gifImage)
        {
            List<Image> gifFrames = new List<Image>();

            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                gifImage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                imageData = ms.ToArray();
            }

            using (MagickImageCollection frames = new MagickImageCollection())
            {
                frames.Ping(imageData);
                frames.Read(imageData);

                foreach (MagickImage frame in frames)
                {
                    using (System.IO.MemoryStream frameStream = new System.IO.MemoryStream())
                    {
                        frame.Write(frameStream, MagickFormat.Png);
                        frameStream.Position = 0;

                        Image frameImage = Image.FromStream(frameStream);
                        gifFrames.Add(frameImage);
                    }
                }
            }

            return gifFrames;
        }
    }
}
