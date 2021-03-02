using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASCIIGenerator
{
  /// <summary>
  /// Checks the extension of a loaded image.
  /// </summary>
  public class ExtensionChecks
  {
    /// <summary>
    /// Checks if the given image is a .jpg, .bmp or .png.
    /// </summary>
    /// <param name="imagePath"> File name of the image. </param>
    /// <returns> True if the extension is .jpg, .bmp or .png, false otherwise. </returns>
    public bool IsImage(string imagePath)
    {
      var ext = Path.GetExtension(imagePath).ToLower();
      return (ext == ".jpg" || ext == ".bmp" || ext == ".png");
    }

    /// <summary>
    /// Checks if the given image is a .gif.
    /// </summary>
    /// <param name="imagePath"> File name of the image. </param>
    /// <returns> True if the extension is .gif, false otherwise. </returns>
    public bool IsGif(string imagePath)
    {
      var ext = Path.GetExtension(imagePath).ToLower();
      return (ext == ".gif");
    }
  }
}
