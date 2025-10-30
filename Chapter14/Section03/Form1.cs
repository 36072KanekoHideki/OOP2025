using System.Diagnostics;
using System.Threading.Tasks;

namespace Section03 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private async void  button1_Click(object sender, EventArgs e) {
            statusStrip1.Text = "";
            var elapsed = await Task.Run(()=> DoLongTimeWokr());
            toolStripStatusLabel1.Text = $"{elapsed}ミリ秒";
        }
        //戻り値のあるメソッド
        private long DoLongTimeWokr() {
            var sw = Stopwatch.StartNew();

            System.Threading.Thread.Sleep(5000);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
