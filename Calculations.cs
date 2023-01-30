using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal class Calculations
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

                        // TODO calculate the trend - not working yet!
                        DateTime middleTime = currentTime.AddHours(-1 * Config.HOURS_TO_KEEP / 2);
                        IEnumerable<decimal> earlierReadings = deviceReadings.Where(
                            r => (r.Time <= middleTime) && (r.Time >= earliestTimeToKeep)).Select(
                            r => r.Rainfall);
                        IEnumerable<decimal> laterReadings = deviceReadings.Where(
                            r => (r.Time <= currentTime) && (r.Time >= middleTime)).Select(
                            r => r.Rainfall);

                        // calculate aggregate data for this device
                        device.MaxRainfall = rainfallValuesForThiDevice.Max();
                        device.AverageRainfall = rainfallValuesForThiDevice.Average();

                        // calculate the trend
                        var earlierAverage = earlierReadings.Average();
                        var laterAverage = laterReadings.Average();

                        device.Trend = laterAverage - earlierAverage;
                    }
                }
            }
            catch (Exception e)
            {
                PrintError("performing calculations", e);
                return;
            }
            // generate alerts
            // I know I could have put this inside the previous for loop,
            // but I prefer to keep the data aggregation & filtering separate from the 
            // alert criteria / display formatting
            try
            {
                // loop through each device and alert type
                foreach (Device device in devices)
                {
                    foreach (AlertType alertType in alertTypes)
                    {
                        // find the valu of the property used by this alert
                        decimal value;

                        try
                        {
                            PropertyInfo propertyToTest = device.GetType().GetProperty(alertType.Property);
                            value = (decimal)propertyToTest.GetValue(device);
                        }
                        catch (Exception e)
                        { 
                            Console.Error.WriteLine("unable to generate alert - missing property: " + e.Message);
                            break;
                        }

                        bool meetsCriteria = (value >= alertType.Min) && (value <= alertType.Max);

                        // if the criteria is met then generate an alert
                        if (meetsCriteria)
                        {
                            PrintAlert(alertType, device, value);

                            // exit the loop so we generate a maximum of one alert per device
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                PrintError("generating alerts", e);
                return;
            }
        }

        public static void PrintError(string context, Exception error)
        {
            Console.Error.WriteLine("Error while " + context + " : " + error.Message);
        }

        public static void PrintAlert(AlertType alertType, Device device, decimal value)
        {
            ConsoleColor backgroundColour;
            Enum.TryParse<ConsoleColor>(alertType.Colour, true, out backgroundColour);
            //Type colourType = Type.GetType("ConsoleColor").GetProperty(alertType.Colour);
            //ConsoleColor backgroundColour = (ConsoleColor)Activator.CreateInstance(colourType);

            // set the console colours to the colour for this alert type
            Console.WriteLine();
            Console.BackgroundColor = backgroundColour;
            Console.ForegroundColor = Config.FOREGROUND_ALERT_COLOUR;
            Console.WriteLine(alertType.AlertName + " for " + device.DeviceName + " located in " + device.Location);

            // reset the console colours back to normal
            Console.BackgroundColor = Config.DEFAULT_BACKGROUND_COLOUR;
            Console.ForegroundColor = Config.DEFAULT_FOREGROUND_COLOUR;
            Console.WriteLine(alertType.Property + " is " + value);
            Console.WriteLine("Trend is " + device.Trend);

        }
    }
}
