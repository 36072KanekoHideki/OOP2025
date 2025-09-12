using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        Color loadClor = Color.FromRgb(0, 0, 0);　　//起動時のカラー
        MyColor currentColor;  //現在設定している色情報

        public MainWindow() {
            InitializeComponent();
            DataContext = GetColorList();

        }
        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                           .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        //すべてのスライダーから呼ばれるイベントハンドラ
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            //colorAreaの色(背景色)は、スライダーで指定したRGBの色を表示する
            currentColor = new MyColor {
                Color = Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value),
                Name = ((MyColor[])DataContext).Where(c => c.Color.R == (byte)rSlider.Value &&
                                                      c.Color.G == (byte)gSlider.Value &&
                                                      c.Color.B == (byte)bSlider.Value).Select(x => x.Name).FirstOrDefault()
            };

            colorArea.Background = new SolidColorBrush(currentColor.Color);
        }

        private void stockList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (!stockList.Items.Contains(currentColor)) {
                stockList.Items.Insert(0, currentColor);
            } else {
                MessageBox.Show("既に登録済みです", "ColorChecker", MessageBoxButton.OK, MessageBoxImage.Warning);
                colorSelectComboBox.SelectedItem = null;
            }
        }

        private void stockButton_Click(object sender, RoutedEventArgs e) {

            foreach (MyColor item in stockList.Items) {
                if (item.Color.R == currentColor.Color.R &&
                    item.Color.G == currentColor.Color.G &&
                    item.Color.B == currentColor.Color.B) {
                    MessageBox.Show("この色はすでに保存されています。", "error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            stockList.Items.Insert(0, currentColor);
        }

        private void colorSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (((ComboBox)sender).SelectedIndex == -1) return;

            setSliderValue(((MyColor)((ComboBox)sender).SelectedItem).Color);
            currentColor = (MyColor)((ComboBox)sender).SelectedItem;
        }

        private void setSliderValue(Color color) {
            rSlider.Value = color.R;
            gSlider.Value = color.G;
            bSlider.Value = color.B;
        }

        //windowは表示されるタイミングで呼ばれる
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            colorSelectComboBox.SelectedIndex = GetColorToIndex(loadClor);  //起動時に設定する色を選択
        }

        //色情報から色配列のインデックスを取得する
        private int GetColorToIndex(Color color) {
            return ((MyColor[])DataContext).ToList().FindIndex(c => c.Color.Equals(color));
        }
    }
}
