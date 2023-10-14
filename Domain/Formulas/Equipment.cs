using Domain.Common;

namespace Domain.Formulas;

public class Equipment
{
    public Image Image { get; set; }
    public Title Title { get; set; }
    public Description Description { get; set; }

    public Equipment(Image image, string title, string description)
    {
        Image = image;
        Title = new Title(title);
        Description = new Description(description);
    }
}