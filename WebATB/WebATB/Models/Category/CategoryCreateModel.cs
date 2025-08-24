using System.ComponentModel.DataAnnotations;

namespace WebATB.Models.Category;

public class CategoryCreateModel
{
    [Display(Name = "Назва")]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "Оберіть файл для фото")]
    public IFormFile? Image { get; set; }
}
