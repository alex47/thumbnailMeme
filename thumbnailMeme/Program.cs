using System;
using System.Drawing;

namespace thumbnailMeme
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Missing input!");
                Console.WriteLine("Usage: thumbnailMeme.exe imagename.jpg outputfolder");
                return;
            }

            const int thumbnailSize = 90;
            const int rowWidth = 117;
            const int colWidth = 110;

            Bitmap inputImage = resizedImage(new Bitmap(args[0]));
            String outputFolder = args[1];

            for (int rowIndex = 0; rowIndex < 6; rowIndex++)
            {
                for (int colIndex = 0; colIndex < 6; colIndex++)
                {
                    Rectangle cropRectangle = new Rectangle(colIndex * colWidth, rowIndex * rowWidth, thumbnailSize, thumbnailSize);
                    cropRectangle.Intersect(new Rectangle(0, 0, inputImage.Width, inputImage.Height));

                    if (cropRectangle.IsEmpty)
                    {
                        continue;
                    }

                    Bitmap thumbnailImage = new Bitmap(thumbnailSize, thumbnailSize);
                    Graphics graphicImage = Graphics.FromImage(thumbnailImage);
                    graphicImage.DrawImage(inputImage, new Rectangle(0, 0, cropRectangle.Width, cropRectangle.Height), cropRectangle, GraphicsUnit.Pixel);
                    thumbnailImage.Save(outputFolder + @"\" + Convert.ToChar('A' + rowIndex) + (colIndex + 1) + ".png");
                }
            }
        }

        static Bitmap resizedImage(Bitmap img)
        {
            const int targetWidth = 660;
            const int targetHeight = 702;

            int newWidth;
            int newHeight;

            if (img.Width > img.Height)
            {
                newWidth = targetWidth;
                newHeight = (targetWidth * img.Height) / img.Width;
            } else
            {
                newWidth = (int)(((float)img.Width / img.Height) * targetHeight);
                newHeight = targetHeight;
            }

            return new Bitmap(img, new Size(newWidth, newHeight));
        }
    }
}
