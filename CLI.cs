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

            // read in CSV rainfall data
            try
            {
                DataReader reader = new DataReader();
                devices = reader.GetDevices();
                readings = reader.GetRainfallReadings();
            }
            catch (Exception e)
            {
                PrintError("reading in CSV devices and rainfall data", e);
                return;
            }
            // organise data by device and filter out old data

            // perform calculations
            try
            {

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
                    Console.WriteLine("Number of readings: " + device.NumberOfReadings);
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
