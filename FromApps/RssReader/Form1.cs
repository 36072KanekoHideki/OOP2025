using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        private List<string> favoriteUrls = new List<string>();


        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"主要","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"国内","https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            {"国際","https://news.yahoo.co.jp/rss/topics/world.xml" },
            {"経済","https://news.yahoo.co.jp/rss/topics/business.xml" },
            {"エンタメ","https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
            {"スポーツ","https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"科学","https://news.yahoo.co.jp/rss/topics/science.xml" },
            {"地域","https://news.yahoo.co.jp/rss/categories/local.xml" },
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

        private void btAddFavorite_Click(object sender, EventArgs e) {

            string selectedUrl = getRssUrl(cbUrl.Text);
            string favoriteName = txtFavoriteName.Text.Trim();

            if (string.IsNullOrEmpty(favoriteName)) {
                MessageBox.Show("お気に入りの名前を入力してください。");
                return;
            }

            // 名前の重複チェック
            if (rssUrlDict.ContainsKey(favoriteName)) {
                MessageBox.Show("この名前はすでに使用されています。別の名前を入力してください。");
                return;
            }

            // URLの重複チェック（同じURLが別名で登録されるのは許容するかはお好みで）
            if (!favoriteUrls.Contains(selectedUrl)) {
                favoriteUrls.Add(selectedUrl);
            }

            rssUrlDict.Add(favoriteName, selectedUrl);

            // コンボボックスを更新
            cbUrl.DataSource = null;
            cbUrl.DataSource = rssUrlDict.Select(k => k.Key).ToList();

            // テキストボックスをクリア
            txtFavoriteName.Text = "";

        }

        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {
            var idx = e.Index;                                                      //描画対象の行
            if (idx == -1) return;                                                  //範囲外なら何もしない
            var sts = e.State;                                                      //セルの状態
            var fnt = e.Font;                                                       //フォント
            var _bnd = e.Bounds;                                                    //描画範囲(オリジナル)
            var bnd = new RectangleF(_bnd.X, _bnd.Y, _bnd.Width, _bnd.Height);     //描画範囲(描画用)
            var txt = (string)lbTitles.Items[idx];                                  //リストボックス内の文字
            var bsh = new SolidBrush(lbTitles.ForeColor);                           //文字色
            var sel = (DrawItemState.Selected == (sts & DrawItemState.Selected));   //選択行か
            var odd = (idx % 2 == 1);                                               //奇数行か
            var fore = Brushes.WhiteSmoke;                                         //偶数行の背景色
            var bak = Brushes.AliceBlue;                                           //奇数行の背景色

            e.DrawBackground();                                                     //背景描画

            //奇数項目の背景色を変える（選択行は除く）
            if (odd && !sel) {
                e.Graphics.FillRectangle(bak, bnd);
            } else if (!odd && !sel) {
                e.Graphics.FillRectangle(fore, bnd);
            }

            //文字を描画
            e.Graphics.DrawString(txt, fnt, bsh, bnd);
        }
    }
}


