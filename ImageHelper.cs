using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;

namespace RemoteClient02
{
  public static class ImageHelper
  {
    private static Dictionary<byte[], ImageFormat> imageFormatDecoders = new Dictionary<byte[], ImageFormat>()
    {
      {
        new byte[8]
        {
          (byte) 137,
          (byte) 80,
          (byte) 78,
          (byte) 71,
          (byte) 13,
          (byte) 10,
          (byte) 26,
          (byte) 10
        },
        ImageFormat.Png
      },
      {
        new byte[2]{ (byte) 66, (byte) 77 },
        ImageFormat.Bmp
      },
      {
        new byte[6]
        {
          (byte) 71,
          (byte) 73,
          (byte) 70,
          (byte) 56,
          (byte) 55,
          (byte) 97
        },
        ImageFormat.Gif
      },
      {
        new byte[6]
        {
          (byte) 71,
          (byte) 73,
          (byte) 70,
          (byte) 56,
          (byte) 57,
          (byte) 97
        },
        ImageFormat.Gif
      },
      {
        new byte[2]{ byte.MaxValue, (byte) 216 },
        ImageFormat.Jpeg
      }
    };

    public static string GetContentType(this byte[] imageBytes)
    {
      ImageFormat imageType = imageBytes.GetImageType();
      if (imageType == ImageFormat.Bmp)
        return "image/x-ms-bmp";
      if (imageType == ImageFormat.Jpeg)
        return "image/jpeg";
      if (imageType == ImageFormat.Gif)
        return "image/gif";
      return imageType == ImageFormat.Png ? "image/png" : "text/html";
    }

    public static ImageFormat GetImageType(this byte[] imageBytes)
    {
      foreach (KeyValuePair<byte[], ImageFormat> imageFormatDecoder in ImageHelper.imageFormatDecoders)
      {
        if (((IEnumerable<byte>) imageFormatDecoder.Key).SequenceEqual<byte>(((IEnumerable<byte>) imageBytes).Take<byte>(imageFormatDecoder.Key.Length)))
          return imageFormatDecoder.Value;
      }
      throw new ArgumentException("Imagetype is unknown!");
    }
  }
}
