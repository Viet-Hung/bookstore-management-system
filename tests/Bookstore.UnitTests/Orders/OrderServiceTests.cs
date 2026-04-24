using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Catalog.Models;
using Bookstore.Module.Orders.Interfaces;
using Bookstore.Module.Orders.Models;
using Bookstore.Module.Orders.Services;
using Bookstore.Module.Orders.ViewModels;
using FluentAssertions;
using Moq;

namespace Bookstore.UnitTests.Orders;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _bookRepositoryMock = new Mock<IBookRepository>();

        _orderService = new OrderService(
            _orderRepositoryMock.Object,
            _bookRepositoryMock.Object);
    }

    [Fact]
    public void Create_WhenStockIsEnough_ShouldCreateOrderAndDecreaseStock()
    {
        // Arrange
        var book = new Book
        {
            Id = 1,
            Title = "Clean Code",
            Price = 20,
            StockQuantity = 10,
            IsActive = true
        };

        var model = new CreateOrderViewModel
        {
            Items = new List<CreateOrderItemViewModel>
            {
                new()
                {
                    BookId = 1,
                    Quantity = 2
                }
            }
        };

        _bookRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(book);

        Order? createdOrder = null;

        _orderRepositoryMock
            .Setup(x => x.Add(It.IsAny<Order>()))
            .Callback<Order>(order => createdOrder = order);

        // Act
        var result = _orderService.Create(model, out var errorMessage);

        // Assert
        result.Should().BeTrue();
        errorMessage.Should().BeEmpty();

        book.StockQuantity.Should().Be(8);

        createdOrder.Should().NotBeNull();
        createdOrder!.Items.Should().HaveCount(1);
        createdOrder.TotalAmount.Should().Be(40);

        _orderRepositoryMock.Verify(x => x.Add(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void Create_WhenStockIsNotEnough_ShouldReturnFalse()
    {
        // Arrange
        var book = new Book
        {
            Id = 1,
            Title = "Clean Code",
            Price = 20,
            StockQuantity = 1,
            IsActive = true
        };

        var model = new CreateOrderViewModel
        {
            Items = new List<CreateOrderItemViewModel>
            {
                new()
                {
                    BookId = 1,
                    Quantity = 2
                }
            }
        };

        _bookRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(book);

        // Act
        var result = _orderService.Create(model, out var errorMessage);

        // Assert
        result.Should().BeFalse();
        errorMessage.Should().Contain("Not enough stock");

        book.StockQuantity.Should().Be(1);

        _orderRepositoryMock.Verify(x => x.Add(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public void Cancel_WhenOrderExists_ShouldCancelAndRestoreStock()
    {
        // Arrange
        var book = new Book
        {
            Id = 1,
            Title = "Clean Code",
            Price = 20,
            StockQuantity = 8,
            IsActive = true
        };

        var order = new Order
        {
            Id = 1,
            Status = OrderStatus.Completed,
            Items = new List<OrderItem>
            {
                new()
                {
                    BookId = 1,
                    Book = book,
                    Quantity = 2,
                    UnitPrice = 20
                }
            }
        };

        _orderRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(order);

        // Act
        var result = _orderService.Cancel(1, out var errorMessage);

        // Assert
        result.Should().BeTrue();
        errorMessage.Should().BeEmpty();

        order.Status.Should().Be(OrderStatus.Cancelled);
        book.StockQuantity.Should().Be(10);

        _orderRepositoryMock.Verify(x => x.Update(order), Times.Once);
    }

    [Fact]
    public void Cancel_WhenOrderAlreadyCancelled_ShouldReturnFalse()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            Status = OrderStatus.Cancelled
        };

        _orderRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(order);

        // Act
        var result = _orderService.Cancel(1, out var errorMessage);

        // Assert
        result.Should().BeFalse();
        errorMessage.Should().Contain("already cancelled");

        _orderRepositoryMock.Verify(x => x.Update(It.IsAny<Order>()), Times.Never);
    }
}