using System;
using CefSharp;

namespace myu_live_sub_view
{
    public class CMyMenuHandler : IContextMenuHandler
    {
        Form1 _form1;
        public CMyMenuHandler(Form1 form1)
        {
            _form1 = form1;
        }
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            // 初期化
            model.Clear();

            // メニューは以下に追加
            // model.AddCheckItem((CefMenuCommand)26501, "自動更新");
            //model.AddSeparator();
            model.AddItem((CefMenuCommand)26502, "再表示(F5)");
            model.AddItem((CefMenuCommand)26503, "更新(Ctrl + F5)");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26504, "戻る(Alt + 左)");
            model.AddItem((CefMenuCommand)26505, "進む(Alt + 右)");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26506, "縦長で初期化");
            model.AddItem((CefMenuCommand)26507, "横長で初期化");
            // model.AddSeparator();
            // model.AddItem((CefMenuCommand)26503, "Display alert message");

            // Check the Check item.
            model.SetChecked((CefMenuCommand)26501, true);
         }
        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
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
                _form1.Invoke(new Action(_form1.ResetPortrait));
                return true;
            }
            // 横長で初期化
            if (commandId == (CefMenuCommand)26507)
            {
                _form1.Invoke(new Action(_form1.ResetSideways));
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
