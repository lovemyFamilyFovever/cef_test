using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEF_test
{
    public partial class Form1 : Form
    {
        public CefSharp.WinForms.ChromiumWebBrowser chromeBrowser;
        public Form1()
        {
            InitializeComponent();
            InitializeChromium();
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            chromeBrowser.RegisterJsObject("cefCustomObject", new CefCustomObject(chromeBrowser, this));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //chromeBrowser.ShowDevTools();
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            String path = string.Format(@"{0}\pugongying\index.html",Application.StartupPath);
            if (!File.Exists(path))
            {
                MessageBox.Show("找不到文件" + path);
            }
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser(path)
            {
                MenuHandler = new CefMenuHandler()
            }; 

            this.panel1.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            chromeBrowser.GetBrowser().CloseBrowser(true);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           // chromeBrowser.ExecuteScriptAsync("showTest();");
        }
    }
}
