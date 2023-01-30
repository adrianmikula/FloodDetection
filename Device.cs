using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal class Device
    {
        public int? DeviceID { get; set; }
        public string? DeviceName { get; set; }
        public string? Location { get; set; }
        public decimal? TotalRainfall { get; set; }
        public decimal? AverageRainfall { get; set; }
        public int NumberOfReadings { get; set; }



    }
}
