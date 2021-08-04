using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace customers.tests
{
    [TestClass]
    public class DatabaseTests
    {
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
    }
}