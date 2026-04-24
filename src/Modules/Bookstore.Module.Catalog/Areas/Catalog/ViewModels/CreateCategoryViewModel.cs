using System.ComponentModel.DataAnnotations;

namespace Bookstore.Module.Catalog.ViewModels;

public class CreateCategoryViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}