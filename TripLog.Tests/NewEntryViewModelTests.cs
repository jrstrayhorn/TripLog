using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;

namespace TripLog.Tests
{
    [TestFixture]
    public class NewEntryViewModelTests
    {
        NewEntryViewModel _vm;

        [SetUp]
        public void Setup()
        {
            var locMock = new Mock<ILocationService>();
            locMock
            .Setup(x => x.GetGeoCoordinatesAsync())
                .ReturnsAsync(
                    new GeoCoords
                    {
                        Latitude = 123,
                        Longitude = 321
                    }
                );

            _vm = new NewEntryViewModel(
                new Mock<INavService>().Object,
                locMock.Object,
                new Mock<ITripLogDataService>().Object);
        }

        [Test]
        public async Task Init_EntryIsSetWithGeoCoordinates()
        {
            // Arrange
            _vm.Latitude = 0.0;
            _vm.Longitude = 0.0;

            // Act
            await _vm.Init();

            // Asset
            Assert.AreEqual(123, _vm.Latitude);
            Assert.AreEqual(321, _vm.Longitude);
        }
    }
}
