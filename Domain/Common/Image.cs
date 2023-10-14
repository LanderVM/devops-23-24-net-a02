namespace Domain.Common;

public class Image
{
    public string ImageUrl { get; set; } // TODO discuss how we'll be storing an image
    public string AltText { get; set; }

    public Image(string imageUrl, string altText)
    {
        ImageUrl = imageUrl;
        AltText = altText;
    }
}