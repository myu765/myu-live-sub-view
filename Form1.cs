using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
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

            Size size = new Size(384, 972);
            ClientSize = size;
            panel1.Size = size;
            Text = "たて | myu-live-sub-view";
            TopMost = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // ブラウザの設定変更
            CefSettings settings = new CefSettings();
            var ci = CultureInfo.CurrentCulture;
            settings.Locale = ci.TwoLetterISOLanguageName;
            settings.AcceptLanguageList = ci.Name;
            settings.LogSeverity = LogSeverity.Disable;
            String cache_path = string.Format(@"{0}\cache", Application.StartupPath);
            settings.CachePath = cache_path;
            String dldata_path = string.Format(@"{0}\download", Application.StartupPath);
            settings.UserDataPath = dldata_path;
            Cef.Initialize(settings);

            // ブラウザを追加
            String index_page = string.Format(@"{0}\resources\index.html", Application.StartupPath);
            WebBrowser = new ChromiumWebBrowser(index_page);
            WebBrowser.BackColor = Color.White;
            WebBrowser.Dock = DockStyle.Fill;
            WebBrowser.MenuHandler = cMyMenuHandler;
            WebBrowser.DownloadHandler = new DownloadHandler();
            panel1.Controls.Add(WebBrowser);
            panel1.Visible = true;
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
            WebBrowser.Load(string.Format(@"{0}\resources\index.html", Application.StartupPath));
        }
        public void ResetPortraitB()
        {
            Size size = new Size(384, 972);
            Text = "たて | myu-live-sub-view";
            ClientSize = size;
            panel1.Size = size;
            WebBrowser.Load(string.Format(@"{0}\resources\index.html", Application.StartupPath));
        }
        // 横長で初期化
        public void ResetSideways()
        {
            Size size = new Size(1600, 180);
            Text = "よこ | myu-live-sub-view";
            ClientSize = size;
            panel1.Size = size;
            WebBrowser.Load(string.Format(@"{0}\resources\index_yoko.html", Application.StartupPath));
        }
        public void ResetSidewaysB()
        {
            Size size = new Size(1536, 216);
            Text = "よこ | myu-live-sub-view";
            ClientSize = size;
            panel1.Size = size;
            WebBrowser.Load(string.Format(@"{0}\resources\index_yoko.html", Application.StartupPath));
        }
        // 全て選択してコピー
        public void PressCtrlAandC()
        {
            // すべて選択(Ctrl+A)
            SendKeys.SendWait("^a");
            // ちょっと待機
            Thread.Sleep(25);
            // コピー(Ctrl+C)
            SendKeys.SendWait("^c");
        }
        // 常に最前面に表示
        public void TglTopMost()
        {
            // 常に最前面に表示の無効化と有効化を切り替える
            TopMost = !TopMost;
        }
    }
}
