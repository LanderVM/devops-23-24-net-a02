namespace Domain.Common;

public class Image
{
    public string ImageUrl { get; set; } // TODO blob storage
    public string AltText { get; set; }

    public Image(string imageUrl, string altText)
    {
        ImageUrl = imageUrl;
        AltText = altText;
    }
}