using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace myu_live_sub_view
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser WebBrowser;
        CMyMenuHandler cMyMenuHandler;

        public Form1()
        {
            InitializeComponent();
            cMyMenuHandler = new CMyMenuHandler(this);

            // クライアント領域を、320 x 900 に固定する
            Size size = new Size(320, 900);
            ClientSize = size;
            panel1.Size = size;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 画面初期化
            ResetPortrait();

            // ブラウザの設定変更
            CefSettings settings = new CefSettings();
            var ci = CultureInfo.CurrentCulture;
            settings.Locale = ci.TwoLetterISOLanguageName;
            settings.AcceptLanguageList = ci.Name;
            settings.LogSeverity = LogSeverity.Disable;
            String cache_path = string.Format(@"{0}\cache", Application.StartupPath);
            settings.CachePath = cache_path;
            Cef.Initialize(settings);

            // ブラウザを追加
            String index_page = string.Format(@"{0}\resources\index.html", Application.StartupPath);
            WebBrowser = new ChromiumWebBrowser(index_page);
            WebBrowser.BackColor = Color.White;
            WebBrowser.Dock = DockStyle.Fill;
            WebBrowser.MenuHandler = cMyMenuHandler;
            panel1.Controls.Add(WebBrowser);
            panel1.Visible = true;

            //
            WebBrowser.LoadingStateChanged += OnLoadingStateChanged;
        }
        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (!args.IsLoading)
            {
                args.Browser.MainFrame.ExecuteJavaScriptAsync("document.body.style.overflow = 'hidden'");
            }
        }
        // 縦長で初期化
        public void ResetPortrait()
        {
            Size size = new Size(320, 900);
            Text = "たて | myu-live-sub-view";
            ClientSize = size;
            panel1.Size = size;
        }
        // 横長で初期化
        public void ResetSideways()
        {
            Size size = new Size(1600, 180);
            Text = "よこ | myu-live-sub-view";
            ClientSize = size;
            panel1.Size = size;
        }
    }
}
