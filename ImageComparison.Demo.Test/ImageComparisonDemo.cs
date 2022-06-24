using FluentAssertions;

namespace ImageComparison.Demo.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BaseAndComparisonAreNotSameDifferenceCreated()
        {
            //Arrange
            string baseImage = ImageFilePath("Image1_Base.jpg", FolderSelector.Base);
            string comparisonImage = ImageFilePath("Image1_Comparison.jpg", FolderSelector.Comparison);
            string differnceImage = ImageFilePath("Image1", FolderSelector.Difference);
            ImageComparison imageComparison = new ImageComparison();

            //Act
            var result = imageComparison.CompareImage(baseImage, comparisonImage, differnceImage);

            //Assert
            result.Should().NotBe(0);
        }

        [Test]
        public void BaseAndComparisonImageAreNotSameWithMaskNoDifference()
        {
            //Arrange
            string baseImage = ImageFilePath("Image2_Base.png", FolderSelector.Base);
            string comparisonImage = ImageFilePath("Image2_Comparison.png", FolderSelector.Comparison);
            string maskImage = ImageFilePath("Image2_Mask.png", FolderSelector.Mask);
            string differnceImage = ImageFilePath("Image2", FolderSelector.Difference);
            ImageComparison imageComparison = new ImageComparison();

            //Act
            var result = imageComparison.CompareImage(baseImage, comparisonImage, differnceImage, maskImage);

            //Assert
            result.Should().Be(0);
        }

        [Test]
        public void BaseAndComparisonAreSameNoDifference()
        {
            //Arrange
            string baseImage = ImageFilePath("Image3_Base.jpg", FolderSelector.Base);
            string comparisonImage = ImageFilePath("Image3_Comparison.jpg", FolderSelector.Comparison);
            string differnceImage = ImageFilePath("Image3", FolderSelector.Difference);
            ImageComparison imageComparison = new ImageComparison();

            //Act
            var result = imageComparison.CompareImage(baseImage, comparisonImage, differnceImage);

            //Assert
            result.Should().Be(0);
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