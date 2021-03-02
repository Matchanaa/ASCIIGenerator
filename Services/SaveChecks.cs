using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASCIIGenerator
{
  /// <summary>
  /// Checks whether the user would like to export their image, and does so.
  /// </summary>
  public class SaveChecks
  {
    /// <summary>
    /// Allows the user to save the output image to a text file, which is saved to the same location and with the same name as the source image.
    /// </summary>
    /// <param name="frameOfASCII"> Output of the ASCII Selector as a string. </param>
    /// <param name="imagePath"> Location of the original source image. </param>
    public void FrameSaveCheck(string frameOfASCII, string imagePath)
    {
      Console.WriteLine("\nSave to .txt file? (Y/N)");
      string response = Console.ReadLine();
      if (response.Equals("y", StringComparison.OrdinalIgnoreCase) || response.Equals("yes", StringComparison.OrdinalIgnoreCase))
      {
        //Generates the full output path from the original source image path. Writes the ASCII to a text file by that name.
        string path = Path.GetDirectoryName(imagePath);
        string imageName = ((Path.GetFileNameWithoutExtension(imagePath) + ".txt"));
        string outputPath = Path.Combine(path, imageName);
        File.WriteAllText(outputPath, frameOfASCII);
      }
    }

    /// <summary>
    /// Allows the user to save the output gif frames to a text file, which is saved to the same location and with the same name as the source image.
    /// </summary>
    /// <param name="splitGif"> Output of the ASCII Selector as a list of strings, one string per frame. </param>
    /// <param name="imagePath"> Location of the original source image. </param>
    public void GifSaveCheck(List<string> splitGif, string imagePath)
    {
      Console.Clear();
      Console.WriteLine("Save to .txt file? (Y/N)");
      string response = Console.ReadLine();
      if (response.Equals("y", StringComparison.OrdinalIgnoreCase) || response.Equals("yes", StringComparison.OrdinalIgnoreCase))
      {
        //Generates the full output path from the original source image path. Writes the ASCII to a text file by that name.
        string path = Path.GetDirectoryName(imagePath);
        string imageName = ((Path.GetFileNameWithoutExtension(imagePath) + ".txt"));
        string outputPath = Path.Combine(path, imageName);

        //Adds each frame, separated by a line break.
        for (int index = 0; index < splitGif.Count; index++)
        {
          File.AppendAllText(outputPath, splitGif[index]);
          File.AppendAllText(outputPath, "\n");
        }
      }
    }
  }
}
