using System;
using System.Windows.Forms;
using CefSharp;

namespace myu_live_sub_view
{
    public class CMyMenuHandler : IContextMenuHandler
    {
        Form1 _form1;
        public int windowType { get; set; }
        public CMyMenuHandler(Form1 form1)
        {
            _form1 = form1;
            windowType = 1;
        }
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            // 初期化
            model.Clear();

            // メニューは以下に追加
            // model.AddCheckItem((CefMenuCommand)26501, "自動更新");
            model.AddItem((CefMenuCommand)26501, "全て選択してコピー (textarea)");
            model.AddSeparator();
            //model.AddItem((CefMenuCommand)26502, "再表示");
            model.AddItem((CefMenuCommand)26503, "更新");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26504, "戻る");
            model.AddItem((CefMenuCommand)26505, "進む");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26506, "初期化 縦長  320 x 900");
            model.AddItem((CefMenuCommand)26507, "初期化 縦長  384 x 972");
            model.AddItem((CefMenuCommand)26508, "初期化 横長 1600 x 180");
            model.AddItem((CefMenuCommand)26509, "初期化 横長 1536 x 216");
            // model.AddSeparator();
            // model.AddItem((CefMenuCommand)26503, "Display alert message");

            // ウィンドウの状態にチェックをつける
            model.SetChecked((CefMenuCommand)26506, false);
            model.SetChecked((CefMenuCommand)26507, false);
            model.SetChecked((CefMenuCommand)26508, false);
            model.SetChecked((CefMenuCommand)26509, false);
            switch ( windowType )
            {
                case 0:
                    model.SetChecked((CefMenuCommand)26506, true);
                    break;
                case 1:
                    model.SetChecked((CefMenuCommand)26507, true);
                    break;
                case 2:
                    model.SetChecked((CefMenuCommand)26508, true);
                    break;
                case 3:
                    model.SetChecked((CefMenuCommand)26509, true);
                    break;
                default:
                    break;
            }
        }
        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            // 全て選択してコピー
            if (commandId == (CefMenuCommand)26501)
            {
                _form1.Invoke(new Action(_form1.PressCtrlAandC));
                return true;
            }
            // 再表示
            if (commandId == (CefMenuCommand)26502)
            {
                browser.Reload(false);
                return true;
            }
            // 更新
            if (commandId == (CefMenuCommand)26503)
            {
                browser.Reload(true);
                return true;
            }
            // 戻る
            if (commandId == (CefMenuCommand)26504)
            {
                if (browser.CanGoBack)
                {
                    browser.GoBack();
                    return true;
                }
            }
            // 進む
            if (commandId == (CefMenuCommand)26505)
            {
                if (browser.CanGoForward)
                {
                    browser.GoForward();
                    return true;
                }
            }
            // 縦長で初期化
            if (commandId == (CefMenuCommand)26506)
            {
                windowType = 0;
                _form1.Invoke(new Action(_form1.ResetPortrait));
                return true;
            }
            // 縦長で初期化
            if (commandId == (CefMenuCommand)26507)
            {
                windowType = 1;
                _form1.Invoke(new Action(_form1.ResetPortraitB));
                return true;
            }
            // 横長で初期化
            if (commandId == (CefMenuCommand)26508)
            {
                windowType = 2;
                _form1.Invoke(new Action(_form1.ResetSideways));
                return true;
            }
            // 横長で初期化
            if (commandId == (CefMenuCommand)26509)
            {
                windowType = 3;
                _form1.Invoke(new Action(_form1.ResetSidewaysB));
                return true;
            }
            return false;
        }
        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            // なし
        }
        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
