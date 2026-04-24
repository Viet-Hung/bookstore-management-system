using Bookstore.Infrastructure.Data;
using Bookstore.Module.Orders.Interfaces;
using Bookstore.Module.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly BookstoreDbContext _context;

    public OrderRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public List<Order> GetAll()
    {
        return _context.Orders
            .Include(x => x.Items)
            .ThenInclude(x => x.Book)
            .OrderByDescending(x => x.OrderDate)
            .ToList();
    }

    public Order? GetById(int id)
    {
        return _context.Orders
            .Include(x => x.Items)
            .ThenInclude(x => x.Book)
            .FirstOrDefault(x => x.Id == id);
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }
}