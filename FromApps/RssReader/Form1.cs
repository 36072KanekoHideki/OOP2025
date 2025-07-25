using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"��v","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"����","https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            {"����","https://news.yahoo.co.jp/rss/topics/world.xml" },
            {"�o��","https://news.yahoo.co.jp/rss/topics/business.xml" },
            {"�G���^��","https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
            {"�X�|�[�c","https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"�Ȋw","https://news.yahoo.co.jp/rss/topics/science.xml" },
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

                //RSS��͂��ĕK�v�ȗv�f�����擾
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();


                //���X�g�{�b�N�X�փ^�C�g����\��
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�f�[�^�Ȃ�"));
            }
        }

        //�R���{�{�b�N�X�̕�������`�F�b�N���ăA�N�Z�X�\��URL��ԋp����
        private string getRssUrl(string str) {

            if (rssUrlDict.ContainsKey(str)) {
                return rssUrlDict[str];
            }
            return str;
        }

        //�^�C�g����I��(�N���b�N)�����Ƃ��ɌĂ΂��C�x���g�n���h��
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


