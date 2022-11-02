
namespace AirQuality.Integration
{
    using Core.DependencyInjection;
    using AirQuality.Core.Integration;
    using ApiModel.Common;
    using ApiModel.LatestMeasurements;
    using Http;
    using Url;
    using Newtonsoft.Json;

    public interface IAirQualityService
    {
        Task<IntegrationResult<ResponseModel<LatestMeasurement>>> GetAirQualityForCityAsync(string city);
    }

    [RegisterScoped]
    public class AirQualityService : IAirQualityService
    {
        private readonly IOpenAqHttpClientFactory _openAqHttpClientFactory;
        private readonly IUrlFactory _urlFactory;

        public AirQualityService(IOpenAqHttpClientFactory openAqHttpClientFactory, IUrlFactory urlFactory)
        {
            _openAqHttpClientFactory = openAqHttpClientFactory;
            _urlFactory = urlFactory;
        }

        public async Task<IntegrationResult<ResponseModel<LatestMeasurement>>> GetAirQualityForCityAsync(string city)
        {
            try
            {
                var path = "v2/latest";
                var url = _urlFactory.CreateForLatestMeasurements(path, city);

                var client = _openAqHttpClientFactory.Create();
                var httpResult = await client.GetAsync(url);
                if (httpResult.IsSuccessStatusCode)
                {
                    var content = await httpResult.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ResponseModel<LatestMeasurement>>(content);
                    return new IntegrationResult<ResponseModel<LatestMeasurement>>(true, data);
                }
                else
                {
                    return new IntegrationResult<ResponseModel<LatestMeasurement>>(false, null,
                        "Integration status incorrect");
                }
            }
            catch (Exception e)
            {
                // log exception using logging tool
                return new IntegrationResult<ResponseModel<LatestMeasurement>>(false, null, "Internal error");
            }
        }
    }
}