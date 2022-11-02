

namespace AirQuality.Integration.Http
{
    using Core.DependencyInjection;
    using Consts;
    using Microsoft.Extensions.Configuration;

    public interface IOpenAqHttpClientFactory
    {
        HttpClient Create();
    }

    [RegisterSingleton]
    public class OpenAqHttpClientFactory : IOpenAqHttpClientFactory
    {
        private readonly string _host;
        private readonly HttpClient _httpClient;

        public OpenAqHttpClientFactory(IConfiguration configuration)
        {
            _host = configuration[AppSettingConsts.AirQualityApiHost];
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_host);
        }

        public HttpClient Create()
        {
            return _httpClient;
        }
    }
}