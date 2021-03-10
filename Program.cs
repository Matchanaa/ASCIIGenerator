using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace ASCIIGenerator
{
  class Program
  {
    /// <summary>
    /// Has the user input a file path, and tests that the file and path are compatible.
    /// Produces a resized ASCII rendering of the image, and provides an option to output the rendering in a text file.
    /// </summary>
    static void Main(string[] args)
    {
      Console.WriteLine("Thank you for using this program! \nFor best results, " +
        "I recommend: \n-Fullscreen Window, \n-Background colour White, \n-Text colour Black, " +
        "\n-Font size 10 Consolas. \nPlease enter the full file path of your .jpg, .bmp, .png or .gif image...\n");
      
      //Reads in the selected file location. Checks if the file is compatible with the program.
      string imagePath = Console.ReadLine();

      //Reads in the image from user selected file location as a Bitmap.
      Bitmap readImage = null;
      try
      {
        readImage = new Bitmap(imagePath);
      }
      catch (ArgumentException)
      {
        Console.WriteLine("File loading failed. Is your file definitely a valid .jpg, .bmp, .png or .gif image?" +
          "\nPress any key to close this window...");
        Environment.Exit(1);
      }

      //Gets an instance of the Luminance values list. Uses this and the image to generate the ASCII rendering.
      var config = new Luminances();
      var imageConverter = new ImageConverter(config);

      //Allows .txt exporting methods to be called.
      var saveChecks = new SaveChecks();

      //Allows checks that differentiate image extensions to be called.
      var extensionChecks = new ExtensionChecks();

      try
      {
        //Checks if the path resolves.
        if (File.Exists(imagePath))
        {

          //Checks the image extension for compatibility.
          if (extensionChecks.IsImage(imagePath))
          {
            //Calculates an appropriate scaling for the selected image. Corrects for possibility of scale being less than zero.
            int scale = readImage.Width / 150;
            if (scale <= 0) scale = 1;

            //Resizes image based on valid scalar.
            var resizedImage = new Bitmap(readImage, new Size(readImage.Width / scale, readImage.Height / scale));

            //Converts the image to ASCII art pixel by pixel and writes it to console as a single string.
            string frameOfASCII = imageConverter.ReadPixels(resizedImage);
            Console.Write(frameOfASCII);

            //Allows user to save the output.
            saveChecks.FrameSaveCheck(frameOfASCII, imagePath);
          }

          //If the image is not a .png, .bmp, or .jpg, checks if the image is .gif.
          else if (extensionChecks.IsGif(imagePath))
          {
            //The list that each ASCII generated frame of the gif will be added to.
            List<string> splitGif = new List<string>();

            //Calculates an appropriate scaling for the selected frame. Corrects for possibility of scale being less than zero.
            int scale = readImage.Width / 100;
            if (scale <= 0) scale = 1;

            //Gets the number of frames in the .gif.
            int gifLength = readImage.GetFrameCount(FrameDimension.Time);

            //Converts each frame to ASCII art pixel by pixel and writes it to console, one string per frame.
            for (int index = 0; index < gifLength; index++)
            {
              //Picks the next frame.
              readImage.SelectActiveFrame(FrameDimension.Time, index);

              //Creates a new bitmap from the selected frame of the original .gif, and scales it to an appropriate size.
              var resizedImage = new Bitmap(readImage, new Size(readImage.Width / scale, readImage.Height / scale));

              //Converts the resized frame and stores the resulting ASCII string in the list.
              string frameOfASCII = imageConverter.ReadPixels(resizedImage);
              splitGif.Add(frameOfASCII);
            }

            //Plays each frame on a loop until space is pressed.
            PlayGif(splitGif, gifLength);
            
            //Gives the user the option to save the ASCII strings to a .txt.
            saveChecks.GifSaveCheck(splitGif, imagePath);
          }
          else  
            Console.WriteLine("The selected file was not a .jpg, .bmp, .png or .gif.");

        }
        else 
          Console.WriteLine("The file path could not be found.");

      }
      catch (Exception e) 
      {
        //Unexpected error
        Console.WriteLine(e); 
      }
    }

    /// <summary>
    /// 'Plays' the '.gif' by writing each frame to console on top of the previous, pausing between each frame to give a roughly 15 fps timing.
    /// Writes each frame in sequence, looping until the spacebar is pressed.
    /// </summary>
    /// <param name="splitGif"> List of ASCII strings, for each frame of the .gif </param>
    /// <param name="gifLength"> Number of frames in the .gif </param>
    public static void PlayGif(List<string> splitGif, int gifLength)
    {
      //Clears the window to allow each frame to render at the same position.
      Console.Clear();
      Console.WriteLine("Press spacebar to continue...");

      //Listens for a key press and checks that it is space. Done in this way to prevent keys being entered into terminal during GifSaveCheck, while allowing '.gif' to loop.
      while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar))
      {
        for (int index = 0; index < gifLength; index++)
        {
          //Writes the ASCII string for the currently selected frame.
          Console.WriteLine(splitGif[index]);

          //Approximately 15 frames per second (1000ms/15), a typical .gif speed.
          Thread.Sleep(67);
          Console.SetCursorPosition(0, 1);
        }
      }
    }
  }
}
