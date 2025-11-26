using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace TenkiApp {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            SetBackgroundByTime();
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += (s, e) => {
                timer.Stop();
                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
                SimpleView.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            };
            timer.Start();
            _ = LoadWeatherAsync();
        }

        private async Task<(double lat, double lon, string city)> GetLocationAsync() {
            using var http = new HttpClient();
            try {
                var location = await http.GetFromJsonAsync<LocationResponse>("http://ip-api.com/json/?lang=ja");
                if (location != null && location.status == "success") {
                    return (location.lat, location.lon, location.city);
                }
            }
            catch { }
            return (35.0, 139.0, "東京");
        }

        private async Task LoadWeatherAsync() {
            using var http = new HttpClient();
            try {
                var (lat, lon, city) = await GetLocationAsync();
                string url =
                    $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}" +
                    $"&current=temperature_2m,wind_speed_10m,relative_humidity_2m" +
                    $"&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,weathercode" +
                    $"&hourly=temperature_2m,weathercode" +
                    $"&timezone=Asia/Tokyo";

                var weather = await http.GetFromJsonAsync<WeatherResponse>(url);

                if (weather?.current != null) {
                    CityText.Text = city;
                    CityTextDetail.Text = city;
                    DateText.Text = $"取得時刻：{weather.current.time}";
                    TemperatureText.Text = $"{weather.current.temperature_2m} ℃";
                    TemperatureTextDetail.Text = $"{weather.current.temperature_2m} ℃";

                    // 風速を風速パネルに表示
                    WindSpeedTextDetail.Text = $"{weather.current.wind_speed_10m} m/s";

                    DescriptionText.Text =
                        $"風速: {weather.current.wind_speed_10m} m/s, 湿度: {weather.current.relative_humidity_2m} %";

                    SetWeatherIcon(weather.current.weathercode, WeatherIcon);
                    SetWeatherIcon(weather.current.weathercode, WeatherIconDetail);

                    ShowWeeklyForecast(weather);
                    ShowHourlyForecast(weather);

                    var timer = new System.Windows.Threading.DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(3);
                    timer.Tick += (s, e) => {
                        timer.Stop();
                        SwitchToDetailView();
                    };
                    timer.Start();
                } else {
                    DescriptionText.Text = "データが取得できませんでした。";
                }
            }
            catch (Exception ex) {
                DescriptionText.Text = $"エラー: {ex.Message}";
            }
        }


        private async Task<(double lat, double lon, string address)> GeocodeAsync(string query) {
            using var http = new HttpClient();
            string url = $"https://nominatim.openstreetmap.org/search?format=json&q={query}&accept-language=ja&limit=1";
            http.DefaultRequestHeaders.UserAgent.ParseAdd("TenkiApp/1.0");

            var json = await http.GetStringAsync(url);
            var results = JsonSerializer.Deserialize<JsonElement>(json);

            if (results.GetArrayLength() > 0) {
                var first = results[0];
                double lat = double.Parse(first.GetProperty("lat").GetString());
                double lon = double.Parse(first.GetProperty("lon").GetString());
                string displayName = first.GetProperty("display_name").GetString();
                return (lat, lon, displayName);
            }
            return (35.0, 139.0, "東京都");
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e) {
            string query = SearchBox.Text.Trim();
            if (string.IsNullOrEmpty(query)) return;

            var (lat, lon, address) = await GeocodeAsync(query);
            await LoadWeatherAsync(lat, lon, address);
        }

        private async Task LoadWeatherAsync(double lat, double lon, string city) {
            using var http = new HttpClient();
            try {
                string url =
                    $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}" +
                    $"&current=temperature_2m,wind_speed_10m,relative_humidity_2m" +
                    $"&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,weathercode" +
                    $"&hourly=temperature_2m,weathercode" +
                    $"&timezone=Asia/Tokyo";

                var weather = await http.GetFromJsonAsync<WeatherResponse>(url);

                if (weather?.current != null) {
                    CityText.Text = city;
                    CityTextDetail.Text = city;
                    DateText.Text = $"取得時刻：{weather.current.time}";
                    TemperatureText.Text = $"{weather.current.temperature_2m} ℃";
                    TemperatureTextDetail.Text = $"{weather.current.temperature_2m} ℃";
                    DescriptionText.Text =
                        $"風速: {weather.current.wind_speed_10m} m/s, 湿度: {weather.current.relative_humidity_2m} %";

                    SetWeatherIcon(weather.current.weathercode, WeatherIcon);
                    SetWeatherIcon(weather.current.weathercode, WeatherIconDetail);

                    ShowWeeklyForecast(weather);
                    ShowHourlyForecast(weather);
                }
            }
            catch (Exception ex) {
                DescriptionText.Text = $"エラー: {ex.Message}";
            }
        }

        private void SetWeatherIcon(int code, Image target) {
            string iconFile = "cloudy.png";
            switch (code) {
                case 0: iconFile = "sun.png"; break;
                case 1: iconFile = "Mainly-clear.png"; break;
                case 2: iconFile = "cloudy.png"; break;
                case 3: iconFile = "rain.png"; break;
            }
            string uri = $"pack://application:,,,/Images/{iconFile}";
            target.Source = new BitmapImage(new Uri(uri));
        }

        private void ShowWeeklyForecast(WeatherResponse weather) {
            WeeklyForecastPanelVertical.Children.Clear();
            if (weather?.daily?.time == null) return;
            int count = weather.daily.time.Length;
            for (int i = 0; i < count; i++) {
                if (weather.daily.temperature_2m_max == null || i >= weather.daily.temperature_2m_max.Length) continue;
                if (weather.daily.temperature_2m_min == null || i >= weather.daily.temperature_2m_min.Length) continue;
                if (weather.daily.weathercode == null || i >= weather.daily.weathercode.Length) continue;
                string dayOfWeek = "";
                if (DateTime.TryParse(weather.daily.time[i], out DateTime date)) {
                    dayOfWeek = date.ToString("ddd");
                }
                var row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(8) };
                row.Children.Add(new TextBlock {
                    Text = dayOfWeek,
                    Width = 50,
                    FontSize = 18,
                    Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 120)),
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center
                });
                var iconImage = new Image { Width = 32, Height = 32, Margin = new Thickness(5, 0, 5, 0) };
                SetWeatherIcon(weather.daily.weathercode[i], iconImage);
                row.Children.Add(iconImage);
                row.Children.Add(new TextBlock {
                    Text = $"{weather.daily.temperature_2m_max[i]}℃",
                    FontSize = 18,
                    Foreground = Brushes.Black,
                    Margin = new Thickness(15, 0, 15, 0),
                    VerticalAlignment = VerticalAlignment.Center
                });
                row.Children.Add(new TextBlock {
                    Text = $"{weather.daily.temperature_2m_min[i]}℃",
                    FontSize = 18,
                    Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 120)),
                    VerticalAlignment = VerticalAlignment.Center
                });
                WeeklyForecastPanelVertical.Children.Add(row);
            }
        }

        private void ShowHourlyForecast(WeatherResponse weather) {
            HourlyForecastPanel.Children.Clear();
            if (weather?.hourly?.time == null ||
                weather.hourly.temperature_2m == null ||
                weather.hourly.weathercode == null) return;

            DateTime nowLocal = DateTime.Now;
            var futureSlots = new List<int>();

            for (int i = 0; i < weather.hourly.time.Length; i++) {
                if (i >= weather.hourly.temperature_2m.Length || i >= weather.hourly.weathercode.Length) continue;
                if (!DateTimeOffset.TryParse(weather.hourly.time[i], out var dto)) continue;
                var dtLocal = dto.ToLocalTime().DateTime;

                if (dtLocal >= nowLocal) {
                    futureSlots.Add(i);
                }
            }

            if (futureSlots.Count == 0) {
                int start = Math.Max(0, weather.hourly.time.Length - 12);
                futureSlots.AddRange(Enumerable.Range(start, weather.hourly.time.Length - start));
            }

            futureSlots = futureSlots.Take(12).ToList();

            bool first = true;
            foreach (int i in futureSlots) {
                DateTimeOffset.TryParse(weather.hourly.time[i], out var dto);
                var dtLocal = dto.ToLocalTime().DateTime;

                string timeLabel = first ? "現在" : dtLocal.ToString("HH:mm");
                first = false;

                var cell = new StackPanel {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(10),
                    Width = 80
                };

                cell.Children.Add(new TextBlock {
                    Text = timeLabel,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center
                });

                var iconImage = new Image { Width = 32, Height = 32, HorizontalAlignment = HorizontalAlignment.Center };
                SetWeatherIcon(weather.hourly.weathercode[i], iconImage);
                cell.Children.Add(iconImage);

                cell.Children.Add(new TextBlock {
                    Text = $"{weather.hourly.temperature_2m[i]}℃",
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center
                });

                HourlyForecastPanel.Children.Add(cell);
            }
        }

        private void SwitchToDetailView() {
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1.5));
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1.5));

            DetailView.Visibility = Visibility.Visible;

            SimpleView.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            DetailView.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        private void SetBackgroundByTime() {
            int hour = DateTime.Now.Hour;
            if (hour >= 6 && hour < 12) {
                SetMorningBackground();
                BackgroundSelector.SelectedIndex = 0;
            } else if (hour >= 12 && hour < 16) {
                SetNoonBackground();
                BackgroundSelector.SelectedIndex = 1;
            } else if (hour >= 16 && hour < 19) {
                SetEveningBackground();
                BackgroundSelector.SelectedIndex = 2;
            } else {
                SetNightBackground();
                BackgroundSelector.SelectedIndex = 3;
            }
        }

        private void BackgroundSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (BackgroundSelector.SelectedItem is ComboBoxItem item) {
                switch (item.Content.ToString()) {
                    case "朝": SetMorningBackground(); break;
                    case "昼": SetNoonBackground(); break;
                    case "夕方": SetEveningBackground(); break;
                    case "夜": SetNightBackground(); break;
                }
            }
        }

        private void SetMorningBackground() {
            var brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#87CEFA"), 0));
            brush.GradientStops.Add(new GradientStop(Colors.White, 1));
            this.Background = brush;
        }

        private void SetNoonBackground() {
            var brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#00BFFF"), 0));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#E0FFFF"), 1));
            this.Background = brush;
        }

        private void SetEveningBackground() {
            var brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF8C00"), 0));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF69B4"), 0.5));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#9370DB"), 1));
            this.Background = brush;
        }

        private void SetNightBackground() {
            var brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#191970"), 0));
            brush.GradientStops.Add(new GradientStop(Colors.Black, 1));
            this.Background = brush;
        }
    }
}
