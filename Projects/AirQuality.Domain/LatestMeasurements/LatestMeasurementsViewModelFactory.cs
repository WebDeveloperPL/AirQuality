

namespace AirQuality.Domain.LatestMeasurements
{
    using Integration;
    using Core.DependencyInjection;

    public interface ILatestMeasurementsViewModelFactory
    {
        LatestMeasurementsViewModel CreateGetModel();
        Task<LatestMeasurementsViewModel> CreatePostModelAsync(string city);
    }

    [RegisterScoped]
    public class LatestMeasurementsViewModelFactory : ILatestMeasurementsViewModelFactory
    {
        private readonly IAirQualityService _airQualityService;

        public LatestMeasurementsViewModelFactory(IAirQualityService airQualityService)
        {
            _airQualityService = airQualityService;
        }

        public LatestMeasurementsViewModel CreateGetModel()
        {
            var model = new LatestMeasurementsViewModel();
            model.IsPost = false;
            model.IsSuccess = true;
            return model;
        }

        public async Task<LatestMeasurementsViewModel> CreatePostModelAsync(string city)
        {
            var model = new LatestMeasurementsViewModel();
            model.IsPost = true;
            model.City = city;

            if (string.IsNullOrEmpty(city))
            {
                model.IsSuccess = false;
                model.Error = "City name can't be empty";
                return model;
            }

            var integrationResult = await _airQualityService.GetAirQualityForCityAsync(city);
            model.IsSuccess = integrationResult.IsSuccess;
            model.Error = integrationResult.ErrorMessage;
            model.Data = integrationResult.Data;
            return model;
        }
    }
}