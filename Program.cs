using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ASCIIGenerator
{
  class Program
  {
    /// <summary>
    /// Has the user input a file path, and tests that the file and path are compatible.
    /// produces a resized ASCII rendering of the image, and provides an option to output the rendering in a text file.
    /// </summary>
    static void Main(string[] args)
    {
      
      Console.WriteLine("Thank you for using this program! \nFor best results, I recommend: \n-Fullscreen Window, \n-Background colour White, \n-Text colour Black, \n-Font size 10 Consolas. \nPlease enter the full file path of your .jpg, .bmp or .png image...\n");
      
      //Reads in the selected file location. Checks if the file is compatible with the program.
      string imagePath = Console.ReadLine();

      try
      {
        //Checks if the path resolves.
        if (File.Exists(imagePath))
        {

          //Checks the image extension for compatibility.
          if (IsImage(imagePath))
          {
            //Reads in the image from user selected file location as a Bitmap.
            var readImage = new Bitmap(imagePath);

            //Calculates an appropriate scaling for the selected image. Corrects for possibility of scale being less than zero.
            int scale = readImage.Width / 150;
            if (scale <= 0) scale = 1;

            //Resizes image based on valid scalar.
            var resizedImage = new Bitmap(readImage, new Size(readImage.Width / scale, readImage.Height / scale));

            //Gets an instance of the Luminance values list. Uses this and the image to generate the ASCII rendering.
            var config = new Luminances();
            var imageConverter = new ImageConverter(config);
            string saveToString = imageConverter.ReadPixels(resizedImage);

            //Allows user to save the output.
            SaveCheck(saveToString, imagePath);
          }

          else
          {
            Console.WriteLine("The selected file was not a .jpg, .bmp or .png.");
          }
        }
        else
        {
          Console.WriteLine("The file path could not be found.");
        }
      }
      catch (Exception e) //Unexpected error
      {
        Console.WriteLine(e);
      }
    }

    /// <summary>
    /// Checks if the given image is a .jpg, .bmp or .png.
    /// </summary>
    /// <param name="imagePath"> File name of the image. </param>
    /// <returns></returns>
    public static bool IsImage(string imagePath)
    {
      var ext = Path.GetExtension(imagePath).ToLower();
      return (ext == ".jpg" || ext == ".bmp" || ext == ".png");
    }

    /// <summary>
    /// Allows the user to save the output to a text file, which is saved to the same location and with the same name as the source image.
    /// </summary>
    /// <param name="saveToString"> Output of the ASCII Selecter as a string. </param>
    /// <param name="imagePath"> Location of the original source image. </param>
    public static void SaveCheck(string saveToString, string imagePath)
    {
      Console.WriteLine("\nSave to .txt file? (Y/N)");
      string response = Console.ReadLine();
      if (response.Equals("y", StringComparison.OrdinalIgnoreCase) || response.Equals("yes", StringComparison.OrdinalIgnoreCase))
      {
        //Generates the full output path from the original source image path. Writes the ASCII to a text file by that name.
        string path = Path.GetDirectoryName(imagePath);
        string imageName = ((Path.GetFileNameWithoutExtension(imagePath) + ".txt"));
        string outputPath = Path.Combine(path, imageName);
        File.WriteAllText(outputPath, saveToString);
      }
    }
  }
}
