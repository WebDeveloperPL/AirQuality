

namespace AirQuality.Domain.LatestMeasurements
{
    using Integration.ApiModel.Common;
    using AirQuality.Integration.ApiModel.LatestMeasurements;
    public class LatestMeasurementsViewModel
    {
        public LatestMeasurementsViewModel()
        {
            Data = new ResponseModel<LatestMeasurement>();
        }

        public string City { get; set; }
        public bool IsPost { get; set; }
        public bool IsSuccess { get; set; }
        public ResponseModel<LatestMeasurement> Data { get; set; }
        public string Error { get; set; }

    }
}
