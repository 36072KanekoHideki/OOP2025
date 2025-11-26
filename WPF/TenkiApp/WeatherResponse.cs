using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenkiApp {
    public class WeatherResponse {
        public Current? current { get; set; }
        public Daily? daily { get; set; }
        public Hourly? hourly { get; set; }
    }

    public class Current {
        public string? time { get; set; }
        public double temperature_2m { get; set; }
        public double wind_speed_10m { get; set; }
        public double relative_humidity_2m { get; set; }
        public int weathercode { get; set; }   // ← ここを追加
    }

    public class Daily {
        public string[]? time { get; set; }
        public double[]? temperature_2m_max { get; set; }
        public double[]? temperature_2m_min { get; set; }
        public double[]? precipitation_sum { get; set; }
        public int[]? weathercode { get; set; }
    }

    public class Hourly {
        public string[]? time { get; set; }
        public double[]? temperature_2m { get; set; }
        public int[]? weathercode { get; set; }
    }
}