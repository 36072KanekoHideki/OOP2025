using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TenkiApp {
    public partial class MainWindow : Window {
        private const string Url =
            "https://api.open-meteo.com/v1/forecast?latitude=35.0&longitude=139.0&current=temperature_2m,wind_speed_10m,relative_humidity_2m";

        public MainWindow() {
            InitializeComponent();
            SetBackgroundByTime();
            _ = LoadWeatherAsync();

            // ✅ 最初の1秒は背景だけ表示
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Tick += (s, e) => {
                timer.Stop();
                FadeIn(MainGrid, 1.5); // 1.5秒かけてふわっと表示
            };
            timer.Start();
        }

        private void FadeIn(UIElement element, double durationSeconds = 1.0) {
            var fadeIn = new DoubleAnimation {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(durationSeconds)
            };
            element.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }


        private async Task<(double lat, double lon, string city)> GetLocationAsync() {
            using var http = new HttpClient();
            try {
                // IPから位置情報を取得
                var location = await http.GetFromJsonAsync<LocationResponse>("http://ip-api.com/json");
                if (location != null && location.status == "success") {
                    return (location.lat, location.lon, location.city);
                }
            }
            catch { }
            // 失敗した場合は東京をデフォルトに
            return (35.0, 139.0, "東京");
        }

        private async Task LoadWeatherAsync() {
            using var http = new HttpClient();
            try {
                // ✅ 現在地を取得
                var (lat, lon, city) = await GetLocationAsync();

                // ✅ 現在地の緯度経度で天気を取得
                string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,wind_speed_10m,relative_humidity_2m";
                var weather = await http.GetFromJsonAsync<WeatherResponse>(url);

                if (weather?.current != null) {
                    CityText.Text = city;
                    DateText.Text = $"取得時刻：{weather.current.time}";
                    TemperatureText.Text = $"{weather.current.temperature_2m} ℃";
                    DescriptionText.Text =
                        $"風速: {weather.current.wind_speed_10m} m/s, 湿度: {weather.current.relative_humidity_2m} %";

                    if (weather.current.temperature_2m >= 20)
                        WeatherIcon.Text = "☀️";
                    else if (weather.current.relative_humidity_2m > 70)
                        WeatherIcon.Text = "🌧️";
                    else
                        WeatherIcon.Text = "☁️";
                } else {
                    DescriptionText.Text = "データが取得できませんでした。";
                }
            }
            catch (Exception ex) {
                DescriptionText.Text = $"エラー: {ex.Message}";
            }
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

        private void BackgroundSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if (BackgroundSelector.SelectedItem is System.Windows.Controls.ComboBoxItem item) {
                switch (item.Content.ToString()) {
                    case "朝":
                        SetMorningBackground();
                        break;
                    case "昼":
                        SetNoonBackground();
                        break;
                    case "夕方":
                        SetEveningBackground();
                        break;
                    case "夜":
                        SetNightBackground();
                        break;
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