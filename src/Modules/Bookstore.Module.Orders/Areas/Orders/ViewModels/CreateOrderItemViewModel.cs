using System.ComponentModel.DataAnnotations;

namespace Bookstore.Module.Orders.ViewModels;

public class CreateOrderItemViewModel
{
    [Range(1, int.MaxValue)]
    public int BookId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}