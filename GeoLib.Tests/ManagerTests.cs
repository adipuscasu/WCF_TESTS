using GeoLib.Contracts;
using GeoLib.Data;
using GeoLib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GeoLib.Tests
{
    [TestClass]
    public class ManagerTests
    {
        private ZipCode zipCode;

        [TestInitialize]
        public void TestLoad()
        {
            zipCode = new ZipCode
            {
                City = "Lincoln Park",
                State = new State {Abbreviation = "NJ"},
                Zip = "07035"
            };
        }
        [TestMethod]
        public void ZipCodeRetrieval()
        {
            // Arrange
            var mockZipCodeRepository = new Mock<IZipCodeRepository>();
            mockZipCodeRepository.Setup(obj => obj.GetByZip("07035")).Returns(zipCode);
            IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);

            // Act
            var zip = geoService.GetZipInfo("07035");

            // Assert
            Assert.IsTrue(zip.City == "Lincoln Park");
            Assert.IsTrue(zip.State == "NJ");
        }
    }
}
