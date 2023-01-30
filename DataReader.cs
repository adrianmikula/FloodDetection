using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal class DataReader
    {

        private CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // assumes CSV headers match the class property names
            // spaces in the CSV headers will be removed
            HasHeaderRecord = true,
            PrepareHeaderForMatch = args => args.Header.Replace(" ", ""),
            BadDataFound = (context) => Console.Error.WriteLine("bad data found in CSV file"),
            IgnoreBlankLines = true,
            MissingFieldFound = (context) => Console.Error.WriteLine("missing field found in CSV file"),
            Delimiter = Config.CSV_DELIMITER
        };

        public IEnumerable<T> ReadFile<T>(string filePath)
        {
            
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, this.config))
            {
                // TODO make this code more generic. We should be using the generic type here somehow
                csv.Context.RegisterClassMap<DeviceMap>();

                var records = csv.GetRecords<T>().ToList();
                return records;
            }

        }


        public IEnumerable<Device> GetDevices()
        { 
            return this.ReadFile<Device>(Config.DATA_FOLDER + Config.DEVICES_DATA_FILE_NAME);
        }

        public IEnumerable<AlertType> GetAlertTypes()
        {
            return this.ReadFile<AlertType>(Config.DATA_FOLDER + Config.ALERT_TYPES_FILE_NAME);
        }

        public IEnumerable<Reading> GetRainfallReadings()
        {
            // get a list of all data files
            var dataFolder = Config.DATA_FOLDER;
            var dataFiles = Directory.EnumerateFiles(dataFolder).Where(f => 
            {
                return f.StartsWith(Config.DATA_FOLDER + Config.READINGS_FILE_PREFIX)
                && f.EndsWith(".csv");
            });

            // parse the CSV for each data file into an object 
            List<Reading> allReadings = new List<Reading>();
            foreach (string file in dataFiles)
            {
                var readingsFromThisFile = this.ReadFile<Reading>(file);

                // combine all the readings into a single list
                allReadings.AddRange(readingsFromThisFile);
            }
            return allReadings;
        }

    }
}
