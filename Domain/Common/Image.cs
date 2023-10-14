namespace Domain.Common;

public class Image
{
    public string ImageUrl { get; set; }
    public string AltText { get; set; }

    public Image(string imageUrl, string altText)
    {
        ImageUrl = imageUrl;
        AltText = altText;
    }
}