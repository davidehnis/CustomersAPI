using System.Linq;
using customers.api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace customers.tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void Fetch()
        {
            // Arrange
            var customers = new CustomersController();

            // Act
            var fetched = customers.Get();

            // Assert
            Assert.IsTrue(fetched.Any());
        }
    }
}