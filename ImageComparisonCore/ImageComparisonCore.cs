using ImageMagick;

namespace ImageComparison.Core
{
    public class ImageComparisonCore
    {
        /// <summary>
        /// Compare images
        /// </summary>
        /// <param name="baseImageLocation">Location of your base image you want to compare</param>
        /// <param name="comparisonImageLocation">Location of the image you want to compare against</param>
        /// <param name="differenceOutput">Where you want the difference saved</param>
        /// <returns></returns>
        public async Task<ImageComparisonResult> CompareImage(CompareImage comparison)
        {
            var baseImage = new MagickImage(comparison.BaseImage);
            var comparisonImage = new MagickImage(comparison.ComparisonImage);
            if (comparison.ImageMask != null)
            {
                var maskImage = new MagickImage(comparison.ImageMask);

                baseImage.SetReadMask(maskImage);
                comparisonImage.SetReadMask(maskImage);
            }

            using (var differenceImage = new MagickImage())
            {
                var result = baseImage.Compare(comparisonImage, ErrorMetric.Absolute, differenceImage);

                return new ImageComparisonResult
                {
                    NoDifferences = result == 0,
                    Differences = differenceImage.ToByteArray()
                };
            }
        }
    }
}