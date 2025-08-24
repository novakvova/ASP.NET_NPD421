namespace WebATB.Models.Category;

public class CategoryCreateModel
{
    public string Name { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}
