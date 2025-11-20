using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenkiApp {
    public class LocationResponse {
        public string status { get; set; }
        public string city { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
    }
}