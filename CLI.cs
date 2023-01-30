using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal class CLI
    {
        public static void PrintCurrentWarnings()
        {
            //IEnumerable<AlertType> alertTypes;
            IEnumerable<Device> devices;
            IEnumerable<Reading> readings;
            IEnumerable < AlertType > alertTypes;

            // read in CSV rainfall data
            try
            {
                DataReader reader = new DataReader();
                devices = reader.GetDevices();
                readings = reader.GetRainfallReadings();
                alertTypes = reader.GetAlertTypes();
            }
            catch (Exception e)
            {
                PrintError("reading in CSV devices and rainfall data", e);
                return;
            }
            // perform calculations
            try
            {
                // find the current time (using the latest timestamp) and filter out old data
                DateTime currentTime = readings.Select(r => r.Time).Max();
                DateTime earliestTimeToKeep = currentTime.AddHours(-1 * Config.HOURS_TO_KEEP);
                IEnumerable<Reading> recentReadings = readings.Where(
                    r => r.Time > earliestTimeToKeep);

                // organise data by device 
                foreach (Device device in devices)
                {
                    IEnumerable<Reading> deviceReadings = recentReadings.Where(
                        r => r.DeviceID == device.DeviceID);
                    if (deviceReadings.Count() > 0)
                    {
                        IEnumerable<decimal> rainfallValuesForThiDevice = deviceReadings.Select(
                            r => r.Rainfall);

                        // calculate aggregate data for this device
                        device.MaxRainfall = rainfallValuesForThiDevice.Max();
                        device.AverageRainfall = rainfallValuesForThiDevice.Average();
                    }
                }
            }
            catch (Exception e)
            {
                PrintError("performing calculations", e);
                return;
            }
            // generate alerts
            try
            {

            }
            catch (Exception e)
            {
                PrintError("generating alerts", e); 
                return;
            }
            // print response
            try
            {
                foreach (Device device in devices)
                {
                    Console.WriteLine();
                    Console.WriteLine(device.DeviceName + ", " + device.Location);
                    Console.WriteLine("Max Rainfall in the last " + Config.HOURS_TO_KEEP + " hours : " + device.MaxRainfall);
                    Console.WriteLine("Average Rainfall in the last " + Config.HOURS_TO_KEEP + " hours : " + device.AverageRainfall);
                }
            }
            catch (Exception e)
            {
                PrintError("printing response", e);
                return;
            }
        }

        public static void PrintError(string context, Exception error)
        {
            Console.Error.WriteLine("Error while " + context + " : " + error.Message);
        }
    }
}
