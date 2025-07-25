namespace RssReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            btGoBack = new Button();
            btGoForward = new Button();
            cbUrl = new ComboBox();
            btAddFavorite = new Button();
            textBox1 = new TextBox();
            txtFavoriteName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(404, 22);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(74, 31);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbTitles.DrawMode = DrawMode.OwnerDrawFixed;
            lbTitles.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(12, 110);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(290, 571);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            lbTitles.DrawItem += lbTitles_DrawItem;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.White;
            wvRssLink.Location = new Point(308, 110);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(895, 571);
            wvRssLink.TabIndex = 4;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.SourceChanged += wvRssLink_SourceChanged;
            // 
            // btGoBack
            // 
            btGoBack.Location = new Point(12, 69);
            btGoBack.Name = "btGoBack";
            btGoBack.Size = new Size(75, 23);
            btGoBack.TabIndex = 5;
            btGoBack.Text = "戻る";
            btGoBack.UseVisualStyleBackColor = true;
            btGoBack.Click += btGoBack_Click_1;
            // 
            // btGoForward
            // 
            btGoForward.Location = new Point(93, 69);
            btGoForward.Name = "btGoForward";
            btGoForward.Size = new Size(75, 23);
            btGoForward.TabIndex = 6;
            btGoForward.Text = "進む";
            btGoForward.UseVisualStyleBackColor = true;
            btGoForward.Click += btGoForward_Click;
            // 
            // cbUrl
            // 
            cbUrl.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbUrl.FormattingEnabled = true;
            cbUrl.Location = new Point(12, 24);
            cbUrl.Name = "cbUrl";
            cbUrl.Size = new Size(386, 29);
            cbUrl.TabIndex = 7;
            // 
            // btAddFavorite
            // 
            btAddFavorite.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btAddFavorite.Location = new Point(678, 64);
            btAddFavorite.Name = "btAddFavorite";
            btAddFavorite.Size = new Size(142, 29);
            btAddFavorite.TabIndex = 8;
            btAddFavorite.Text = "お気に入り登録";
            btAddFavorite.UseVisualStyleBackColor = true;
            btAddFavorite.Click += btAddFavorite_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBox1.Location = new Point(537, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(40, 29);
            textBox1.TabIndex = 9;
            textBox1.Text = "名前";
            // 
            // txtFavoriteName
            // 
            txtFavoriteName.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtFavoriteName.Location = new Point(583, 24);
            txtFavoriteName.Name = "txtFavoriteName";
            txtFavoriteName.Size = new Size(237, 29);
            txtFavoriteName.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1215, 688);
            Controls.Add(txtFavoriteName);
            Controls.Add(textBox1);
            Controls.Add(btAddFavorite);
            Controls.Add(cbUrl);
            Controls.Add(btGoForward);
            Controls.Add(btGoBack);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(btRssGet);
            Name = "Form1";
            Text = "RSSリーダー";
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button btGoBack;
        private Button btGoForward;
        private ComboBox cbUrl;
        private Button btAddFavorite;
        private TextBox textBox1;
        private TextBox txtFavoriteName;
    }
}
