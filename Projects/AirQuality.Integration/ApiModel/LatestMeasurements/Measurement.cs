using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Integration.ApiModel.LatestMeasurements
{
    public class Measurement
    {
        public string Parameter { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public string Unit { get; set; }
    }
}
