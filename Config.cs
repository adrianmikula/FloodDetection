﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal abstract class Config
    {
        public const string CSV_DELIMITER = ",";
        public const string DATA_FOLDER = "C:\\Source\\Exercises\\Interfuze\\";
        public const string DEVICES_DATA_FILE_NAME = "Devices.csv";
        public const string READINGS_FILE_PREFIX = "Data";
        public const int HOURS_TO_KEEP = 4;
        
    }
}
