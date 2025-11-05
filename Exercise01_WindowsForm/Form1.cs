using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exercise01_WindowsForm {
    public partial class Form1 : Form {
        public Form1() {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitializeComponent();
        }

        private async void btnLoad_Click(object sender, EventArgs e) {
            ofdTextFileOpen.Title = "テキストファイルを選択してください";

            if (ofdTextFileOpen.ShowDialog() == DialogResult.OK) {
                string filePath = ofdTextFileOpen.FileName;
                    string content = await ReadFileAsync(filePath);
                    TextBox1.Text = content;
                }
            }

        private async Task<string> ReadFileAsync(string path) {
            using (var reader = new StreamReader(path, Encoding.GetEncoding("Shift_JIS"))) {
                return await reader.ReadToEndAsync();
            }
        }
    }
}