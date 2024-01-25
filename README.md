# ASCIIGenerator
A simple .jpg/.bmp/.png and .gif to ASCII converter with an option to export the result to a text file. Program reads in the file path of an image and returns an appropriately sized ASCII art version of the image in console, and optionally in a text file.

User selects their image by writing in the full file path of an image with the .jpg, .bmp, .png or .gif extensions, eg. C:\Users\User\Pictures\image.jpg ASCII rendering is displayed in window, and can then be exported as a .txt file to the original image location.

ASCII characters are selected based on the relative luminances of a subset of the printable ASCII. Percentage 'Opacity' (for lack of a better term) was found by calculating the percentage of a character's 8x16 pixels space was taken up by the character's pixels. This 'Opacity', which ranged from 0% for space to 33.59% for @, was mapped proportionally to the full 0-255 range of luminance to get a relative luminance for each character. This allows for characters to be selected appropriately for their luminance, corrected for uneven differences in character luminances. (The difference in luminance between space and - is significantly greater than the difference between n and r, for example.)

The calculation for relative luminance, (0.375 * Red) + (0.5 * Green) + (0.125 * Blue) is an approximation of (0.2126 * R) + (0.7152 * G) + (0.0722 * B), a reflection of how much certain light colours are perceived by the eye.

Date Created: 04/02/21 Last Updated: 02/03/21 Matcha Ennay
