using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirQuality.Integration.ApiModel.Common;

namespace AirQuality.Integration.ApiModel.LatestMeasurements
{
    public class LatestMeasurement
    {
        public string Location { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Coordinates Coordinates { get; set; }
        public List<Measurement> Measurements { get; set; }
    }

}
