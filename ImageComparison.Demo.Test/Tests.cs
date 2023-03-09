using FluentAssertions;
using ImageComparison.Core;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ImageComparison.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task BaseAndComparisonAreNotSameDifferenceCreated()
        {
            //Arrange
            var baseImage = ImageFilePath("Image1_Base.png", FolderSelector.Base);
            var comparisonImage = ImageFilePath("Image1_Comparison.png", FolderSelector.Comparison);
            var comparison = CreateComparison(baseImage, comparisonImage, null);

            ImageComparisonCore imageComparison = new ImageComparisonCore();

            //Act
            var result = await imageComparison.CompareImage(comparison);

            //Assert
            result.NoDifferences.Should().BeFalse();
            result.Differences.Should().NotBeEmpty();
            SaveImage(nameof(BaseAndComparisonAreNotSameDifferenceCreated), result.Differences);
        }

        [Test]
        public async Task BaseAndComparisonImageAreNotSameWithMaskNoDifference()
        {
            //Arrange
            string baseImage = ImageFilePath("Image2_Base.png", FolderSelector.Base);
            string comparisonImage = ImageFilePath("Image2_Comparison.png", FolderSelector.Comparison);
            string maskImage = ImageFilePath("Image2_Mask.png", FolderSelector.Mask);

            var comparison = CreateComparison(baseImage, comparisonImage, maskImage);

            ImageComparisonCore imageComparison = new ImageComparisonCore();

            //Act
            var result = await imageComparison.CompareImage(comparison);

            //Assert
            result.NoDifferences.Should().BeTrue();
            result.Differences.Should().NotBeEmpty();
            SaveImage(nameof(BaseAndComparisonImageAreNotSameWithMaskNoDifference), result.Differences);
        }

        [Test]
        public async Task BaseAndComparisonAreSameNoDifference()
        {
            //Arrange
            string baseImage = ImageFilePath("Image3_Base.jpg", FolderSelector.Base);
            string comparisonImage = ImageFilePath("Image3_Comparison.jpg", FolderSelector.Comparison);
            var comparison = CreateComparison(baseImage, comparisonImage, null);

            ImageComparisonCore imageComparison = new ImageComparisonCore();

            //Act
            var result = await imageComparison.CompareImage(comparison);

            //Assert
            result.NoDifferences.Should().BeTrue();
            result.Differences.Should().NotBeEmpty();
            SaveImage(nameof(BaseAndComparisonAreSameNoDifference),result.Differences);
        }

        private CompareImage CreateComparison(
            string baseImage,
            string comparisonImage,
            string? maskImage)
        {
            return new CompareImage
            {
                BaseImage = ConvertImageToByteArray(baseImage),
                ComparisonImage = ConvertImageToByteArray(comparisonImage),
                ImageMask = ConvertImageToByteArray(maskImage),
            };
        }

        private static void SaveImage(string testName, byte[] imageBytes)
        {
            var directory = $"{AppDomain.CurrentDomain.BaseDirectory}\\Differences";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllBytes($"{directory}\\{testName}.png", imageBytes);

           
            //
            //File.WriteAllBytes($"{directory}\\{testName}.png", imageBytes);
        }

        private static byte[]? ConvertImageToByteArray(string fileName)
        {
            if (fileName == null)
                return null;

            return File.ReadAllBytes(fileName);
        }

        private string ImageFilePath(string imageName, FolderSelector folderSelector)
        {
            switch (folderSelector)
            {
                case FolderSelector.Base:
                    return @$"{Directory.GetCurrentDirectory()}/BaseImages/{imageName}";

                case FolderSelector.Comparison:
                    return @$"{Directory.GetCurrentDirectory()}/ComparisonImages/{imageName}";

                case FolderSelector.Mask:
                    return @$"{Directory.GetCurrentDirectory()}/MaskImages/{imageName}";

                case FolderSelector.Difference:
                    return @$"C:\Temp\Differences\{imageName}.png";

                default:
                    throw new Exception($"No folder path for name {folderSelector}");
            }
        }
    }
}