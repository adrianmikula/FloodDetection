using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal class AlertType
    {
        public string AlertName { get; set; }
        public string Colour { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }

        public string Property { get; set; }
    }
}
