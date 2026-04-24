using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Orders.Interfaces;
using Bookstore.Module.Orders.Models;
using Bookstore.Module.Orders.ViewModels;

namespace Bookstore.Module.Orders.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;

    public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
    }

    public List<Order> GetAll()
    {
        return _orderRepository.GetAll();
    }

    public Order? GetById(int id)
    {
        return _orderRepository.GetById(id);
    }

    public bool Create(CreateOrderViewModel model, out string errorMessage)
    {
        errorMessage = string.Empty;

        var validItems = model.Items
            .Where(x => x.BookId > 0 && x.Quantity > 0)
            .ToList();

        if (!validItems.Any())
        {
            errorMessage = "Order must contain at least one valid item.";
            return false;
        }

        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Completed
        };

        foreach (var item in validItems)
        {
            var book = _bookRepository.GetById(item.BookId);

            if (book == null || !book.IsActive)
            {
                errorMessage = "One or more selected books are invalid.";
                return false;
            }

            if (book.StockQuantity < item.Quantity)
            {
                errorMessage = $"Not enough stock for book: {book.Title}.";
                return false;
            }

            book.StockQuantity -= item.Quantity;

            order.Items.Add(new OrderItem
            {
                BookId = book.Id,
                Quantity = item.Quantity,
                UnitPrice = book.Price
            });
        }

        order.TotalAmount = order.Items.Sum(x => x.Quantity * x.UnitPrice);

        _orderRepository.Add(order);

        return true;
    }

    public bool Cancel(int id, out string errorMessage)
    {
        errorMessage = string.Empty;

        var order = _orderRepository.GetById(id);

        if (order == null)
        {
            errorMessage = "Order was not found.";
            return false;
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            errorMessage = "Order is already cancelled.";
            return false;
        }

        foreach (var item in order.Items)
        {
            if (item.Book != null)
            {
                item.Book.StockQuantity += item.Quantity;
            }
        }

        order.Status = OrderStatus.Cancelled;

        _orderRepository.Update(order);

        return true;
    }
}