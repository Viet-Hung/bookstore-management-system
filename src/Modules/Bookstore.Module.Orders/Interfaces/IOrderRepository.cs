using Bookstore.Module.Orders.Models;

namespace Bookstore.Module.Orders.Interfaces;

public interface IOrderRepository
{
    List<Order> GetAll();
    Order? GetById(int id);
    void Add(Order order);
    void Update(Order order);
}