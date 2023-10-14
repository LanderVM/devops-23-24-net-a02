using Domain.Common;

namespace Domain.Formulas;

public class Equipment
{
    public Image Image { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Equipment(Image image, string title, string description)
    {
        Image = image;
        Title = title;
        Description = description;
    }
}