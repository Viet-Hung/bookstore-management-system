using System.ComponentModel.DataAnnotations;

namespace Bookstore.Module.Catalog.ViewModels;

public class EditCategoryViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}