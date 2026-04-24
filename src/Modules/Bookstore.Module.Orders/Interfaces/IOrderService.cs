using Bookstore.Module.Orders.Models;
using Bookstore.Module.Orders.ViewModels;

namespace Bookstore.Module.Orders.Interfaces;

public interface IOrderService
{
    List<Order> GetAll();
    Order? GetById(int id);
    bool Create(CreateOrderViewModel model, out string errorMessage);
    bool Cancel(int id, out string errorMessage);
}