using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace customers.tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void Add()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 345.ToString(),
                Name = "Marley"
            };

            // Act
            CustomerDatabase.Save(customer);
            var marley = CustomerDatabase.Get(345);

            // Assert
            Assert.IsNotNull(marley);
            Assert.AreEqual("Marley", marley.Name);
            Process.Start("notepad.exe", "database.json");
        }

        [TestMethod]
        public void Fetch()
        {
            // Act
            var customers = CustomerDatabase.Fetch();

            // Assert
            Assert.IsTrue(customers.Any());
        }

        [TestMethod]
        public void Get()
        {
            // Act
            var customer = CustomerDatabase.Get(1);

            // Assert
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public void Save()
        {
            // Arrange
            var customer = CustomerDatabase.Get(1);

            // Act
            customer.Name = "Roberts";
            CustomerDatabase.Save(customer);
            var second = CustomerDatabase.Get(1);

            // Assert
            Assert.AreEqual(second.Name, "Roberts");
            Process.Start("notepad.exe", "database.json");
        }
    }
}