namespace AirQuality.Integration.Url
{
    using Core.DependencyInjection;
    using Microsoft.AspNetCore.WebUtilities;

    public interface IUrlFactory
    {
        string CreateForLatestMeasurements(string path, string city);
    }

    [RegisterScoped]
    public class UrlFactory : IUrlFactory
    {
        public string CreateForLatestMeasurements(string path, string city)
        {
            path = QueryHelpers.AddQueryString(path, "limit", "100");
            path = QueryHelpers.AddQueryString(path, "page", "1");
            path = QueryHelpers.AddQueryString(path, "offset", "0");
            path = QueryHelpers.AddQueryString(path, "sort", "desc");
            path = QueryHelpers.AddQueryString(path, "radius", "1000");
            path = QueryHelpers.AddQueryString(path, "order_by", "lastUpdated");
            path = QueryHelpers.AddQueryString(path, "dumpRaw", "false");
            var cityUpperCase = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);
            path = QueryHelpers.AddQueryString(path, "city", cityUpperCase);
            return path;
        }
    }
}