using ImageMagick;

namespace ImageComparison.Demo.Test
{
    public class ImageComparison
    {
        /// <summary>
        /// Compare images
        /// </summary>
        /// <param name="baseImageLocation">Location of your base image you want to compare</param>
        /// <param name="comparisonImageLocation">Location of the image you want to compare against</param>
        /// <param name="differenceOutput">Where you want the difference saved</param>
        /// <returns></returns>
        public double CompareImage(string baseImageLocation, string comparisonImageLocation, string differenceOutput)
        {
            var baseImage = new MagickImage(baseImageLocation);
            var comparisonImage = new MagickImage(comparisonImageLocation);

            using (var differenceImage = new MagickImage())
            {
                double result = baseImage.Compare(comparisonImage, ErrorMetric.Absolute, differenceImage);
                differenceImage.Write(differenceOutput);
                return result;
            }
        }

        /// <summary>
        /// Compare images
        /// </summary>
        /// <param name="baseImageLocation">Location of your base image you want to compare</param>
        /// <param name="comparisonImageLocation">Location of the image you want to compare against</param>
        /// <param name="differenceImageLocation">Where you want the difference saved</param>
        /// <param name="maskImageLocation">Image mask to remove areas from comparison</param>
        /// <returns></returns>
        public double CompareImage(string baseImageLocation, string comparisonImageLocation, string differenceOutput, string maskImageLocation)
        {
            var baseImage = new MagickImage(baseImageLocation);
            var comparisonImage = new MagickImage(comparisonImageLocation);
            var maskImage = new MagickImage(maskImageLocation);

            baseImage.SetReadMask(maskImage);
            comparisonImage.SetReadMask(maskImage);

            using (var differenceImage = new MagickImage())
            {
                double result = baseImage.Compare(comparisonImage, ErrorMetric.Absolute, differenceImage);
                differenceImage.Write(differenceOutput);
                return result;
            }
        }
    }
}