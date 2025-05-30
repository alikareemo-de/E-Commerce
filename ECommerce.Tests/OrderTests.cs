using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.ValueObjects;
using Xunit;
using Assert = Xunit.Assert;

namespace ECommerce.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Should_Create_Order_With_Valid_Data()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var items = new List<OrderItem>
            {
                new("product-1", 2, new Money(100, "USD")),
                new("product-2", 1, new Money(50, "USD"))
            };

            // Act
            var order = new Order(Guid.NewGuid(), customerId, items);

            // Assert
            Assert.Equal(OrderStatus.Pending, order.OrderStatus);
            Assert.Equal(250, order.TotalPrice.Amount);
        }


        [Fact]
        public void Should_Cancel_Pending_Order()
        {
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), new List<OrderItem>
    {
        new("product-1", 2, new Money(100, "USD"))
    });
            order.ConfigureStateMachine();

            order.Cancel("TEST");

            Assert.Equal(OrderStatus.Cancelled, order.OrderStatus);
        }


        [Fact]
        public void Should_Throw_When_Cancelling_Shipped_Order()
        {
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), new List<OrderItem>
    {
        new("product-1", 1, new Money(50, "USD"))
    });
            order.ConfigureStateMachine();
            order.Confirm();
            order.Ship();

            Assert.Throws<DomainException>(() => order.Cancel("TEST"));
        }

        [Fact]
        public void Should_Confirm_Then_Ship_Then_Deliver_Order()
        {
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), new List<OrderItem>
    {
        new("product-1", 1, new Money(100, "USD"))
    });
            order.ConfigureStateMachine();

            order.Confirm();
            Assert.Equal(OrderStatus.Confirmed, order.OrderStatus);

            order.Ship();
            Assert.Equal(OrderStatus.Shipped, order.OrderStatus);

            order.Deliver();
            Assert.Equal(OrderStatus.Delivered, order.OrderStatus);
        }



        [Fact]
        public void Should_Throw_When_Order_Has_No_Items()
        {
            var customerId = Guid.NewGuid();
            Assert.Throws<DomainException>(() =>
            {
                var order = new Order(Guid.NewGuid(), customerId, new List<OrderItem>());
            });
        }

        [Fact]
        public void Should_Change_Status_To_Confirmed()
        {
            var order = CreateSampleOrder();
            order.Confirm();
            Xunit.Assert.Equal(OrderStatus.Confirmed, order.OrderStatus);
        }

        private Order CreateSampleOrder()
        {
            return new Order(Guid.NewGuid(), Guid.NewGuid(), new List<OrderItem>
            {
                new("product-1", 1, new Money(100, "USD"))
            });
        }


    }
}
