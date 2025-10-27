using CustomerApp.Data;
using SQLite;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
namespace CustomerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private byte[] selectedImageBytes;

    private List<Customer> _customer = new List<Customer>();

    public MainWindow() {
        InitializeComponent();
        ReadDatabase();

        CustomerListView.ItemsSource = _customer;
    }


    private void ReadDatabase() {
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            _customer = connection.Table<Customer>().ToList();
        }
    }


    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        var customer = new Customer() {
            Name = NameTextBox.Text,
            Phone = PhoneTextBox.Text,
            Address = AddressTextBox.Text,
            Picture = selectedImageBytes
        };

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Insert(customer);
        }

        ReadDatabase();
        CustomerListView.ItemsSource = _customer;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        var item = CustomerListView.SelectedItem as Customer;
        if (item == null) {
            MessageBox.Show("行を選択してください");
            return;
        }

        //データベース接続
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Delete(item);
            ReadDatabase();     //データベースから選択されているレコードの削除
            CustomerListView.ItemsSource = _customer;
        }
    }

    //リストビューのフィルタリング
    private void SerchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
        var filterList = _customer.Where(p => p.Name.Contains(SerchTextBox.Text));

        CustomerListView.ItemsSource = filterList;
    }

    //リストビューから１レコード選択
    private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selectedCustomer = CustomerListView.SelectedItem as Customer;
        if (selectedCustomer is null) return;

        NameTextBox.Text = selectedCustomer.Name;
        PhoneTextBox.Text = selectedCustomer.Phone;
        AddressTextBox.Text = selectedCustomer.Address;
        selectedImageBytes = selectedCustomer.Picture;

        if (selectedCustomer.Picture != null) {
            var bitmap = new BitmapImage();
            using (var stream = new MemoryStream(selectedCustomer.Picture)) {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            SelectedImage.Source = bitmap;
        } else {
            SelectedImage.Source = null;
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e) {
        var selectedCustomer = CustomerListView.SelectedItem as Customer;
        if (selectedCustomer is null) return;

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();

            var customer = new Customer() {
                Id = selectedCustomer.Id,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                Picture = selectedImageBytes ?? selectedCustomer.Picture
            };
            connection.Update(customer);

            ReadDatabase();
            CustomerListView.ItemsSource = _customer;
        }
    }

    private void Picture_Click(object sender, RoutedEventArgs e) {
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.Filter = "画像ファイル (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

        if (dialog.ShowDialog() == true) {
            selectedImageBytes = File.ReadAllBytes(dialog.FileName);

            var bitmap = new BitmapImage();
            using (var stream = new MemoryStream(selectedImageBytes)) {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            SelectedImage.Source = bitmap;
        }
    }

    private async void PostalCodeTextBox_TextChanged(object sender, TextChangedEventArgs e) {
        string postalCode = PostalCodeTextBox.Text.Trim();
        if (postalCode.Length == 7 && postalCode.All(char.IsDigit)) {
            try {
                var service = new PostalCodeService();
                var address = await service.GetAddressFromPostalCodeAsync(postalCode);
                if (address != null) {
                    AddressTextBox.Text = $"{address.Prefecture}{address.City}{address.Town}";
                }
            }
            catch (Exception ex) {
                MessageBox.Show("住所の取得に失敗しました: " + ex.Message);
            }
        }
    }

}