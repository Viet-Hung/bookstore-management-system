using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Orders.Interfaces;
using Bookstore.Module.Orders.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Module.Orders.Areas.Orders.Controllers;

[Area("Orders")]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IBookService _bookService;

    public OrdersController(IOrderService orderService, IBookService bookService)
    {
        _orderService = orderService;
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var orders = _orderService.GetAll();
        return View(orders);
    }

    public IActionResult Details(int id)
    {
        var order = _orderService.GetById(id);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new CreateOrderViewModel
        {
            AvailableBooks = _bookService.GetAllBooks()
                .Where(x => x.IsActive && x.StockQuantity > 0)
                .ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateOrderViewModel model)
    {
        model.AvailableBooks = _bookService.GetAllBooks()
            .Where(x => x.IsActive && x.StockQuantity > 0)
            .ToList();

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var created = _orderService.Create(model, out var errorMessage);

        if (!created)
        {
            TempData["ErrorMessage"] = errorMessage;
            return View(model);
        }

        TempData["SuccessMessage"] = "Order created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Cancel(int id)
    {
        var cancelled = _orderService.Cancel(id, out var errorMessage);

        if (!cancelled)
        {
            TempData["ErrorMessage"] = errorMessage;
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Order cancelled successfully.";
        return RedirectToAction(nameof(Index));
    }
}