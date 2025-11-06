using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
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

namespace WebBrowser;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeAsync();
    }

    private async void InitializeAsync() {
        await WebView.EnsureCoreWebView2Async();  //非同期にしてブラウザの初期化処理を行う

        WebView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
        WebView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;


    }

    //読み込み開始したらプログレスバーを表示
    private void CoreWebView2_NavigationStarting(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e) {
        LoadingBar.Visibility = Visibility.Visible;
        LoadingBar.IsIndeterminate = true;
    }

    //読み込み完了したらプログレスバーを非表示
    private void CoreWebView2_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
        LoadingBar.Visibility = Visibility.Collapsed;
        LoadingBar.IsIndeterminate = false;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e) {
        if (WebView.CanGoBack) {
            WebView.GoBack();
        }
    }

    private void FowardButton_Click(object sender, RoutedEventArgs e) {
        if (WebView.CanGoForward) {
            WebView.GoForward();
        }
    }

    private void GoButton_Click(object sender, RoutedEventArgs e) {
        var url = AddressBar.Text;
        if (string.IsNullOrWhiteSpace(url)) return;
        WebView.Source = new Uri(url);
    }
}