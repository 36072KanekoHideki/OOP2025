using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"主要","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"国内","https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            {"国際","https://news.yahoo.co.jp/rss/topics/world.xml" },
            {"経済","https://news.yahoo.co.jp/rss/topics/business.xml" },
            {"エンタメ","https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
            {"スポーツ","https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"科学","https://news.yahoo.co.jp/rss/topics/science.xml" },
            {"Pen online","https://news.yahoo.co.jp/rss/media/penonline/all.xml" },
        };


        public Form1() {
            InitializeComponent();
            wvRssLink.NavigationCompleted += WvRssLink_NavigationCompleted;
        }

        private void Form1_Load_1(object sender, EventArgs e) {
            cbUrl.DataSource = rssUrlDict.Select(k => k.Key).ToList();

            btGoForward.Enabled = false;
            btGoBack.Enabled = false;

        }

        private async void btRssGet_Click(object sender, EventArgs e) {

            using (var hc = new HttpClient()) {

                string xml = await hc.GetStringAsync(getRssUrl(cbUrl.Text));
                XDocument xdoc = XDocument.Parse(xml);

                //RSS解析して必要な要素数を取得
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();


                //リストボックスへタイトルを表示
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "データなし"));
            }
        }

        //コンボボックスの文字列をチェックしてアクセス可能なURLを返却する
        private string getRssUrl(string str) {

            if (rssUrlDict.ContainsKey(str)) {
                return rssUrlDict[str];
            }
            return str;
        }

        //タイトルを選択(クリック)したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);
        }

        private void btGoBack_Click_1(object sender, EventArgs e) {
            wvRssLink.GoBack();
            btGoBack.Enabled = wvRssLink.CanGoBack;
        }


        private void btGoForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }


        private void WvRssLink_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {

        }

        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            btGoBack.Enabled = wvRssLink.CanGoBack;
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }
    }
}


