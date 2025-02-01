// Helpers/ImageComparer.cs
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class ImageComparer
{
    public class ComparisonResult
    {
        public double MatchPercentage { get; set; }
        public bool[,] MatchingPixels { get; set; } = new bool[0,0];
    }

    public async Task<ComparisonResult> CompareImages(string url1, string url2)
    {
        using var httpClient = new HttpClient();
        
        try 
        {
            // Hent begge billeder parallelt
            var task1 = httpClient.GetStreamAsync(url1);
            var task2 = httpClient.GetStreamAsync(url2);
            await Task.WhenAll(task1, task2);

            using var image1 = await Image.LoadAsync<Rgba32>(await task1);
            using var image2 = await Image.LoadAsync<Rgba32>(await task2);

            Console.WriteLine($"Image1 size: {image1.Width}x{image1.Height}");
            Console.WriteLine($"Image2 size: {image2.Width}x{image2.Height}");

            // Normaliser størrelsen til samme dimensioner
            int width = 300;  // Øget størrelse for mere præcision
            int height = 200; // Bevarer aspect ratio

            var pixelMap1 = GetPixelMap(image1, width, height);
            var pixelMap2 = GetPixelMap(image2, width, height);

            return CalculateMatchPercentage(pixelMap1, pixelMap2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CompareImages: {ex.Message}");
            throw;
        }
    }

    private static int[,] GetPixelMap(Image<Rgba32> image, int targetWidth, int targetHeight)
    {
        var map = new int[targetWidth, targetHeight];
        float scaleX = (float)image.Width / targetWidth;
        float scaleY = (float)image.Height / targetHeight;

        for (int y = 0; y < targetHeight; y++)
        {
            for (int x = 0; x < targetWidth; x++)
            {
                int sourceX = Math.Min((int)(x * scaleX), image.Width - 1);
                int sourceY = Math.Min((int)(y * scaleY), image.Height - 1);
                
                var pixel = image[sourceX, sourceY];
                // Konverter til gråtone for mere robust sammenligning
                int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                map[x, y] = grayValue;
            }
        }

        return map;
    }

    private static ComparisonResult CalculateMatchPercentage(int[,] map1, int[,] map2)
    {
        int width = map1.GetLength(0);
        int height = map1.GetLength(1);
        int totalPixels = width * height;
        double totalSimilarity = 0;
        var matchingPixels = new bool[width, height];
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var value1 = map1[x, y];
                var value2 = map2[x, y];
                
                double similarity = CalculatePixelSimilarity(value1, value2);
                totalSimilarity += similarity;
                matchingPixels[x, y] = similarity > 0.5; // Gem hvilke pixels der matcher
            }
        }

        return new ComparisonResult 
        {
            MatchPercentage = (totalSimilarity / totalPixels) * 100,
            MatchingPixels = matchingPixels
        };
    }

    private static double CalculatePixelSimilarity(int value1, int value2)
    {
        // Beregn lighed baseret på forskel i gråtone
        int difference = Math.Abs(value1 - value2);
        const int maxDifference = 50; // Øget tolerance

        if (difference <= maxDifference)
        {
            return 1.0 - ((double)difference / maxDifference);
        }
        return 0;
    }
}