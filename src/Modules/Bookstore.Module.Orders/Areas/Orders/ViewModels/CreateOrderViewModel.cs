using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Orders.ViewModels;

public class CreateOrderViewModel
{
    public List<CreateOrderItemViewModel> Items { get; set; } = new()
    {
        new CreateOrderItemViewModel()
    };

    public List<Book> AvailableBooks { get; set; } = new();
}