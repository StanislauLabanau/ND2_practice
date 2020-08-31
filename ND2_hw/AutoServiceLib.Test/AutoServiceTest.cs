using AutoServiceLib.Interfaces;
using AutoServiceLib.Types;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutoServiceLib.Test
{
    public class AutoServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        class StubDiscount : IDiscount
        {
            public List<Customer> GoldenCustomers { get; set; }

            public decimal GetCalculatedDiscount(decimal total, Customer customer)
            {
                return 0;
            }
        }

        [Test]
        public void GetTotal_CorrectListProvided_CorrectTotalReturned_ValidExperticeArea()
        {
            // Arrange
            var customer = new Customer("Harry", "Potter");
            var car = new Car("Renault", "12345678", new string[] { "engine", "chassis", "body" });
            var order = new OrderForm(customer, car, new string[] { "change motor oil", "change shock absorbers" });
            var operations = new List<IOperation>
            {
                new Operation("engine", "change motor oil", 120),
                new Operation("engine", "change air filter", 30),
                new Operation("chassis", "change shock absorbers", 200),
                new Operation("chassis", "change steering tips", 60),
            };
            var discount = new StubDiscount();
            var autoService = new AutoService("PSA official", discount, operations);

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(320, total);
        }

        [Test]
        public void GetTotal_CorrectListProvided_CorrectTotalReturned_InvalidExperticeArea()
        {
            // Arrange
            var customer = new Customer("Harry", "Potter");
            var car = new Car("Renault", "12345678", new string[] { "engine", "chassis", "body" });
            var order = new OrderForm(customer, car, new string[] { "change motor oil", "change shock absorbers" });
            var operations = new List<IOperation>
            {
                new Operation("electro", "change battary", 540),
                new Operation("electro", "change main contacts", 400),
            };
            var discount = new StubDiscount();
            var autoService = new AutoService("PSA official", discount, operations);

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(0, total);
        }

        [Test]
        public void GetTotal_CorrectListProvided_CorrectTotalReturned_OperationMatchesAbsence()
        {
            // Arrange
            var customer = new Customer("Harry", "Potter");
            var car = new Car("Renault", "12345678", new string[] { "engine", "chassis", "body" });
            var order = new OrderForm(customer, car, new string[] { "change headlight lamps", "setting of headlight" });
            var operations = new List<IOperation>
            {
                new Operation("engine", "change motor oil", 120),
                new Operation("engine", "change air filter", 30),
                new Operation("chassis", "change shock absorbers", 200),
                new Operation("chassis", "change steering tips", 60),
            };
            var discount = new StubDiscount();
            var autoService = new AutoService("PSA official", discount, operations);

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(0, total);
        }

        [Test]
        public void GetTotal_FullDefoulDiscount_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            var customer = new Customer("Harry", "Potter");
            var car = new Car("Renault", "12345678", new string[] { "engine", "chassis", "body" });
            var order = new OrderForm(customer, car, new string[] { "change motor oil", "change shock absorbers" });
            var operations = new List<IOperation>
            {
                new Operation("engine", "change motor oil", 120),
                new Operation("engine", "change air filter", 30),
                new Operation("chassis", "change shock absorbers", 200),
                new Operation("chassis", "change steering tips", 60),
            };

            var goldenCustomers = new List<Customer> { customer };
            var discount = new DefaultDiscount(goldenCustomers);
            var autoService = new AutoService("PSA official", discount, operations);

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(272, total);
        }

        [Test]
        public void GetTotalMoq_CorrectListProvided_CorrectTotalReturned_ValidExperticeArea()
        {
            var goldenCustomer = new Customer("Tom", "Riddle");
            var usualCustomer = new Customer("Harry", "Potter");
            var car = new Car("Renault", "12345678", new string[] { "engine", "chassis", "body" });
            var operations = new List<IOperation>
            {
                new Operation("engine", "change motor oil", 100),
                new Operation("engine", "change air filter", 30),
                new Operation("chassis", "change shock absorbers", 200),
                new Operation("chassis", "change steering tips", 60),
            };

            var mock = new Mock<IDiscount>();

            mock.Setup(m => m.GetCalculatedDiscount(It.IsAny<decimal>(), It.IsAny<Customer>()))
                .Returns(0);

            mock.Setup(m => m.GetCalculatedDiscount(It.Is<decimal>(v => v >= 300), It.IsAny<Customer>()))
                .Returns(15);

            mock.Setup(m => m.GetCalculatedDiscount(It.Is<decimal>(v => v >= 300), goldenCustomer))
               .Returns(45);

            var autoService = new AutoService("PSA official", mock.Object, operations);
            var order = new OrderForm(usualCustomer, car, new string[] { "change motor oil" });
            var zeroDescount = autoService.GetTotalPrice(order);

            order = new OrderForm(usualCustomer, car, new string[] { "change motor oil", "change shock absorbers" });
            var fivePersentDescount = autoService.GetTotalPrice(order);

            order = new OrderForm(goldenCustomer, car, new string[] { "change motor oil", "change shock absorbers" });
            var fifteenPersentDescount = autoService.GetTotalPrice(order);

            Assert.AreEqual(100, zeroDescount);
            Assert.AreEqual(285, fivePersentDescount);
            Assert.AreEqual(255, fifteenPersentDescount);

            mock.Verify(d => d.GetCalculatedDiscount(It.IsAny<decimal>(), It.IsAny<Customer>()), Times.Exactly(3));
        }
    }
}