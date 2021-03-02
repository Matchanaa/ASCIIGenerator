namespace ASCIIGenerator
{
  /// <summary>
  /// Constructors for the Luminance and ASCII character values, contained in the Luminances List.
  /// </summary>
  public class Luminance
  {
    /// <summary>
    /// Defines each Luminance value, an approximation of the amount of red, blue and green light in a pixel.
    /// </summary>
    public int LuminanceValue { get; set; }

    /// <summary>
    /// Defines an ASCII character for each Luminance value.
    /// </summary>
    public string CharValue { get; set; }
  }
}

