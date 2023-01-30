using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal abstract class Config
    {
        public const string CSV_DELIMITER = ",";
        public const string DATA_FOLDER = "C:\\Source\\Exercises\\Interfuze\\FloodDetection\\";
        public const string DEVICES_DATA_FILE_NAME = "Devices.csv";
        public const string ALERT_TYPES_FILE_NAME = "AlertTypes.csv";
        public const string READINGS_FILE_PREFIX = "Data";
        public const int HOURS_TO_KEEP = 4;

        public const ConsoleColor FOREGROUND_ALERT_COLOUR = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_BACKGROUND_COLOUR = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_FOREGROUND_COLOUR = ConsoleColor.White;

    }
}
