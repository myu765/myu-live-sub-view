using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace myu_live_sub_view
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser WebBrowser;
        public Form1()
        {
            InitializeComponent();
            // クライアント領域を、320 x 880 に固定する
            Size size = new Size(320, 880);
            ClientSize = size;
            panel1.Size = size;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ブラウザを追加
            String index_page = string.Format(@"{0}\resources\index.html", Application.StartupPath);
            WebBrowser = new ChromiumWebBrowser(index_page);
            WebBrowser.BackColor = Color.White;
            WebBrowser.Dock = DockStyle.Fill;
            panel1.Controls.Add(WebBrowser);
            panel1.Visible = true;
        }
    }
}
