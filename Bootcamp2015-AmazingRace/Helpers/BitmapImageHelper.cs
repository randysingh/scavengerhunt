using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class BitmapImageHelpers
    {

        /// <summary>
        /// Resize an image to a specified width and height.
        /// Cannot set both width and height to 0. Setting one of them to 0 uses the other one and current aspect ratio of the image.
        /// </summary>
        /// <param name="origFile"></param>
        /// <param name="width">Width of the resized image. Set to 0 to maintain aspect ratio based on specified height</param>
        /// <param name="height">Height of the resized image. Set to 0 to maintain aspect ratio based on specified width</param>
        /// <param name="quality">Jpeg output quality on a scale of 0 - 1</param>
        /// <returns>stream representing new image</returns>
        public static async Task<IRandomAccessStream> ResizeImage(StorageFile origFile, uint width = 0, uint height = 0, double quality = 1.0)
        {
            //additional help:
            // BitmapPropertySet: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/jj709940.aspx
            // Resize image:  https://social.msdn.microsoft.com/Forums/sqlserver/en-US/b4ef6ce3-e2a7-46d7-bf0d-da0d0f0c9d5a/c-metro-resizing-without-quality-reduction
            // Resize image2: http://stackoverflow.com/questions/12349611/how-to-resize-image-in-c-sharp-winrt-winmd
            if (width == 0 && height == 0)
                throw new ArgumentException("Width or Height must be non zero");

            // get width and height from the original image
            IRandomAccessStreamWithContentType stream = await origFile.OpenReadAsync();

            ImageProperties properties = await origFile.Properties.GetImagePropertiesAsync();
            var aspectRatio = (double)properties.Width / (double)properties.Height;
            uint destWidth = width == 0 ? (uint)(height * aspectRatio) : width;
            if (destWidth > properties.Width) destWidth = properties.Width;
            uint destHeight = height == 0 ? (uint)((double)width / aspectRatio) : height;
            if (destHeight > properties.Height) destHeight = properties.Height;

            // create encoder for saving the tile image
            var propertySet = new BitmapPropertySet();
            var qualityValue = new BitmapTypedValue(1.0, Windows.Foundation.PropertyType.Single);
            propertySet.Add("ImageQuality", qualityValue);

            // get proper decoder for the input file - jpg/png/gif
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            var dest = new InMemoryRandomAccessStream();

            var transform = new BitmapTransform() { ScaledHeight = destHeight, ScaledWidth = destWidth };
            var pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Rgba8,
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.DoNotColorManage);

            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, dest, propertySet);
            encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Straight, destWidth, destHeight, decoder.DpiX, decoder.DpiY, pixelData.DetachPixelData());
            await encoder.FlushAsync();

            return dest;
        }

        public static async Task<byte[]> ResizeImageToByteArray(StorageFile origFile, uint width = 0, uint height = 0, double quality = 1.0)
        {
            var ras = await ResizeImage(origFile, width, height, quality);

            var res = new byte[ras.Size];
            DataReader dataReader = new DataReader(ras.GetInputStreamAt(0));
            await dataReader.LoadAsync((uint)ras.Size);

            dataReader.ReadBytes(res);

            return res;
        }

        public static async Task<bool> ResizeImage(StorageFile origFile, StorageFile destFile, uint width = 0, uint height = 0, double quality = 1.0)
        {
            var res = await ResizeImage(origFile, width, height, quality);

            using (IRandomAccessStream destStream = await destFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                await RandomAccessStream.CopyAndCloseAsync(res.GetInputStreamAt(0), destStream.GetOutputStreamAt(0));
            }
            return true;
        }

        public static async Task<BitmapImage> ResizeImageToBitmap(StorageFile origFile, uint width = 0, uint height = 0, double quality = 1.0)
        {
            var res = await ResizeImage(origFile, width, height, quality);
            var bImg = new BitmapImage();
            await bImg.SetSourceAsync(res);
            return bImg;
        }

        public static async Task<Tuple<IRandomAccessStream, BitmapImage, byte[]>> ResizeImageToAllFormats(StorageFile origFile, uint width = 0, uint height = 0, double quality = 1.0)
        {
            var res = await ResizeImage(origFile, width, height, quality);

            var bImg = new BitmapImage();
            await bImg.SetSourceAsync(res);

            var bytes = new byte[res.Size];
            DataReader dataReader = new DataReader(res.GetInputStreamAt(0));
            await dataReader.LoadAsync((uint)res.Size);

            dataReader.ReadBytes(bytes);

            return Tuple.Create(res, bImg, bytes);
        }

    }
}