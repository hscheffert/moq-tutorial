using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Tests
{

    /// <summary>
    /// We have 2 paths to test: whether _paymentService.Charge() answers with true or false
    /// Need to confirm that if we get paid, we ship (assert that shipmentService is called)
    /// </summary>
    public class CartControllerTests
    {
        CartController controller;
        Mock<IPaymentService> paymentServiceMock;
        Mock<IShipmentService> shipmentServiceMock;
        Mock<ICartService> cartServiceMock;
        Mock<ICard> cardMock;
        Mock<IAddressInfo> addressMock;
        List<CartItem> items;

        [SetUp]
        public void Setup()
        {
            paymentServiceMock = new Mock<IPaymentService>();
            shipmentServiceMock = new Mock<IShipmentService>();
            cartServiceMock = new Mock<ICartService>();
            cardMock = new Mock<ICard>();
            addressMock = new Mock<IAddressInfo>();

            var itemMock = new Mock<CartItem>();
            //itemMock.Object.Price = 10;
            // itemMock.Setup(x => x.Price).Returns(10);
            items = new() { itemMock.Object };
            cartServiceMock.Setup(x => x.Items()).Returns(items);

            // Create the controller with the mocks
            controller = new CartController(cartServiceMock.Object, paymentServiceMock.Object, shipmentServiceMock.Object);
        }

        [Test]
        public void ShouldReturnCharged()
        {
            // Charge returns true
            paymentServiceMock.Setup(x => x.Charge(It.IsAny<double>(), cardMock.Object))
                .Returns(true);

            // Call the controller method
            string result = controller.CheckOut(cardMock.Object, addressMock.Object);

            // Verify the Ship method is called once
            shipmentServiceMock.Verify(x => x.Ship(addressMock.Object, items), Times.Once);

            // Assert returns charged
            Assert.AreEqual("charged", result);
        }

        [Test] 
        public void ShouldReturnNotCharged()
        {
            // Charge returns false
            paymentServiceMock.Setup(x => x.Charge(It.IsAny<double>(), cardMock.Object))
                .Returns(false);

            // Call the controller method
            string result = controller.CheckOut(cardMock.Object, addressMock.Object);

            // Verify the Ship method is not called
            shipmentServiceMock.Verify(x => x.Ship(addressMock.Object, items), Times.Never);

            // Assert returns not charged
            Assert.AreEqual("not charged", result);
        }
    }
}
