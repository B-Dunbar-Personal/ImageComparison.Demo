namespace ImageComparison.Core
{
    public class CompareImage
    {
        public byte[] BaseImage { get; set; }

        public byte[] ComparisonImage { get; set; }

        public byte[]? ImageMask { get; set; }
    }
}
