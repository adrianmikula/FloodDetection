using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    class DeviceMap : ClassMap<Device>
    {
       
        public DeviceMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(d => d.AverageRainfall).Ignore().Default(0);
            Map(d => d.MaxRainfall).Ignore().Default(0);
            Map(d => d.Trend).Ignore().Default(0);
            //Map(d => d.NumberOfReadings).Ignore().Default(0);
        }
        
    }
}
