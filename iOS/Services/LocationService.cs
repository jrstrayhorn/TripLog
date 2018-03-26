using System;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Geolocation;

namespace TripLog.iOS.Services
{
    public class LocationService : ILocationService
    {
        public LocationService()
        {
        }

        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            var locator = new Geolocator
            {
                DesiredAccuracy = 30
            };

            var position = await locator.GetPositionAsync(30000);

            var result = new GeoCoords
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };

            return result;
        }
    }
}
