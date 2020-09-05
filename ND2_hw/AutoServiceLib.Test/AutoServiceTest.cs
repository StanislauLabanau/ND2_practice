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
            var customer = new Customer { FirstName = "Harry", SecondName = "Potter" };
            var car = new Car { Model = "Renault", VIN = "12345678", Sections = new string[] { "engine", "chassis", "body" } };
            var order = new OrderForm { Customer = customer, Car = car, Operations = new string[] { "change motor oil", "change shock absorbers" } };
            var operations = new List<IOperation>
            {
                new Operation { Section = "engine", Name = "change motor oil", Price = 120 },
                new Operation { Section = "engine", Name= "change air filter", Price = 30 },
                new Operation { Section = "chassis", Name= "change shock absorbers", Price = 200 },
                new Operation { Section = "chassis", Name = "change steering tips", Price = 60 },
            };
            var stubDiscount = new StubDiscount();
            var autoService = new AutoService { Name = "PSA official", Discount = stubDiscount, Operations = operations };

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(320, total);
        }

        [Test]
        public void GetTotal_CorrectListProvided_CorrectTotalReturned_InvalidExperticeArea()
        {
            // Arrange
            var customer = new Customer { FirstName = "Harry", SecondName = "Potter" };
            var car = new Car { Model = "Renault", VIN = "12345678", Sections = new string[] { "engine", "chassis", "body" } };
            var order = new OrderForm { Customer = customer, Car = car, Operations = new string[] { "change motor oil", "change shock absorbers" } };
            var operations = new List<IOperation>
            {
                new Operation { Section = "electro", Name = "change battary", Price = 540 },
                new Operation { Section = "electro", Name= "change main contacts", Price = 400 },
            };
            var stubDiscount = new StubDiscount();
            var autoService = new AutoService { Name = "PSA official", Discount = stubDiscount, Operations = operations };

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(0, total);
        }

        [Test]
        public void GetTotal_CorrectListProvided_CorrectTotalReturned_OperationMatchesAbsence()
        {
            var customer = new Customer { FirstName = "Harry", SecondName = "Potter" };
            var car = new Car { Model = "Renault", VIN = "12345678", Sections = new string[] { "engine", "chassis", "body" } };
            var order = new OrderForm { Customer = customer, Car = car, Operations = new string[] { "change headlight lamps", "setting of headlight" } };
            var operations = new List<IOperation>
            {
                new Operation { Section = "engine", Name = "change motor oil", Price = 120 },
                new Operation { Section = "engine", Name= "change air filter", Price = 30 },
                new Operation { Section = "chassis", Name= "change shock absorbers", Price = 200 },
                new Operation { Section = "chassis", Name = "change steering tips", Price = 60 },
            };
            var stubDiscount = new StubDiscount();
            var autoService = new AutoService { Name = "PSA official", Discount = stubDiscount, Operations = operations };

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(0, total);
        }

        [Test]
        public void GetTotal_FullDefoulDiscount_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            var goldenMember = new Membership { Title = "goldenMember", DiscountValue = 10m };
            var customer = new Customer { FirstName = "Harry", SecondName = "Potter", Membership = goldenMember };
            var car = new Car { Model = "Renault", VIN = "12345678", Sections = new string[] { "engine", "chassis", "body" } };
            var order = new OrderForm { Customer = customer, Car = car, Operations = new string[] { "change motor oil", "change shock absorbers" } };
            var operations = new List<IOperation>
            {
                new Operation { Section = "engine", Name = "change motor oil", Price = 120 },
                new Operation { Section = "engine", Name= "change air filter", Price = 30 },
                new Operation { Section = "chassis", Name= "change shock absorbers", Price = 200 },
                new Operation { Section = "chassis", Name = "change steering tips", Price = 60 },
            };
            var discount = new DefaultDiscount();
            var autoService = new AutoService { Name = "PSA official", Discount = discount, Operations = operations };

            // Act
            var total = autoService.GetTotalPrice(order);

            // Assert
            Assert.AreEqual(272, total);
        }

        [Test]
        public void GetTotalMoq_CorrectListProvided_CorrectTotalReturned_ValidExperticeArea()
        {
            // Arrange
            var goldenMember = new Membership { Title = "goldenMember", DiscountValue = 10m };
            var customer = new Customer { FirstName = "Tom", SecondName = "Riddle"};
            var goldenCustomer = new Customer { FirstName = "Harry", SecondName = "Potter", Membership = goldenMember };
            var car = new Car { Model = "Renault", VIN = "12345678", Sections = new string[] { "engine", "chassis", "body" } };
            var operations = new List<IOperation>
            {
                new Operation { Section = "engine", Name = "change motor oil", Price = 100 },
                new Operation { Section = "engine", Name= "change air filter", Price = 30 },
                new Operation { Section = "chassis", Name= "change shock absorbers", Price = 200 },
                new Operation { Section = "chassis", Name = "change steering tips", Price = 60 },
            };

            var mock = new Mock<IDiscount>();

            mock.Setup(m => m.GetCalculatedDiscount(It.IsAny<decimal>(), It.IsAny<Customer>()))
                .Returns(0);

            mock.Setup(m => m.GetCalculatedDiscount(It.Is<decimal>(v => v >= 300), It.IsAny<Customer>()))
                .Returns(15);

            mock.Setup(m => m.GetCalculatedDiscount(It.Is<decimal>(v => v >= 300), goldenCustomer))
               .Returns(45);

            var autoService = new AutoService { Name = "PSA official", Discount = mock.Object, Operations = operations };
            var order = new OrderForm { Customer = customer, Car = car, Operations = new string[] { "change motor oil" } };
            var zeroDescount = autoService.GetTotalPrice(order);

            order = new OrderForm { Customer = customer, Car = car, Operations = new string[] { "change motor oil", "change shock absorbers" } };
            var fivePersentDescount = autoService.GetTotalPrice(order);

            order = new OrderForm { Customer = goldenCustomer, Car = car, Operations = new string[] { "change motor oil", "change shock absorbers" } };
            var fifteenPersentDescount = autoService.GetTotalPrice(order);

            Assert.AreEqual(100, zeroDescount);
            Assert.AreEqual(285, fivePersentDescount);
            Assert.AreEqual(255, fifteenPersentDescount);

            mock.Verify(d => d.GetCalculatedDiscount(It.IsAny<decimal>(), It.IsAny<Customer>()), Times.Exactly(3));
        }
    }
}