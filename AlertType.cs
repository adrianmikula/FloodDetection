using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodDetection
{
    internal class AlertType
    {
        public string name { get; set; }
        public string colour { get; set; }
        public decimal threshold { get; set; }

        public string algorithm { get; set; }
    }
}
