using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIGenerator
{
  /// <summary>
  /// Constructors for each of the Luminance values in the Luminances List.
  /// </summary>
  public class Luminance
  {
    /// <summary>
    /// Allows the getting and setting of the Luminance values. 
    /// </summary>
    public int LuminanceValue { get; set; }

    /// <summary>
    /// Allows the getting and setting of ASCII characters for each Luminance.
    /// </summary>
    public string CharValue { get; set; }
  }
}

