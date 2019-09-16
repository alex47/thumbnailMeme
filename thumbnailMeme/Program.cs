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

            Bitmap inputImage = new Bitmap(args[0]);
            String outputFolder = args[1];

            int thumbnailSize = 90;
            int rowWidth = 117;
            int colWidth = 110;

            for (int rowIndex = 0; rowIndex < 6; rowIndex++)
            {
                for (int colIndex = 0; colIndex < 6; colIndex++)
                {
                    Bitmap tempImage = resizedImage(inputImage);
                    Rectangle cropRectangle = new Rectangle(colIndex * colWidth, rowIndex * rowWidth, thumbnailSize, thumbnailSize);

                    cropRectangle.Intersect(new Rectangle(0, 0, tempImage.Width, tempImage.Height));

                    if (cropRectangle.IsEmpty)
                    {
                        continue;
                    }

                    Bitmap thumbnailImage = new Bitmap(thumbnailSize, thumbnailSize);
                    Graphics graphicImage = Graphics.FromImage(thumbnailImage);
                    graphicImage.DrawImage(tempImage, new Rectangle(0, 0, cropRectangle.Width, cropRectangle.Height), cropRectangle, GraphicsUnit.Pixel);
                    thumbnailImage.Save(outputFolder + @"\" + Convert.ToChar('A' + rowIndex) + (colIndex + 1) + ".png");
                }
            }
        }

        static Bitmap resizedImage(Bitmap img)
        {
            int targetWidth = 660;
            int targetHeight = 702;

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
