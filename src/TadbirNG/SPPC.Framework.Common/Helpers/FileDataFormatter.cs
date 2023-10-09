using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SPPC.Framework.Presentation;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// عملیات تبدیل نوع داده های فایل را پیاده سازی می کند
    /// </summary>
    public class FileDataFormatter
    {
        /// <summary>
        /// داده های تصویر مورد نظر را با توجه به ویژگی های داده شده اینکد می کند
        /// </summary>
        /// <returns>داده های تصویر</returns>
        public static byte[] ImageDataFileEncoder(byte[] byteImage, string imageType, ImageFileSpecsDto imageFileSpecs)
        {
            var image = Image.Load<Rgba32>(byteImage);

            if (imageFileSpecs.ResizeHeight != 0 && imageFileSpecs.ResizeWidth != 0)
            {
                image.Mutate(x => x.Resize(imageFileSpecs.ResizeWidth, imageFileSpecs.ResizeHeight));
            }

            using (var memoryStream = new MemoryStream())
            {
                if (imageType == JpegFormat.Instance.DefaultMimeType)
                {
                    var encoder = new JpegEncoder()
                    {
                        Quality = imageFileSpecs.Quality,
                        ColorType = JpegColorType.Cmyk
                    };
                    image.SaveAsync(memoryStream, encoder);
                }
                else if (imageType == PngFormat.Instance.DefaultMimeType)
                {
                    var encoder = new PngEncoder()
                    {

                    };
                    image.SaveAsync(memoryStream, encoder);
                }
                //memoryStream.Position = 0; // The position needs to be reset.

                byteImage = memoryStream.ToArray();
            }

            return byteImage;
        }


        /// <summary>
        /// داده های تصویر مورد نظر را از فایل داده عبوری استخراج می کند
        /// </summary>
        /// <returns>داده های تصویر</returns>
        public static byte[] IFormFileToByteArray(IFormFile formFile)
        {
            byte[] dataFile;
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                dataFile = ms.ToArray();
            }

            return dataFile;
        }


    }
}
