using InstallFontTest.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InstallFontTest
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        [STAThread]
        public static void Main()
        {
            CheckInstallResources();

            var app = new App();
            app.InitializeComponent();

            app.Run();
        }

        private static void CheckInstallResources()
        {
            // Check Font
            FontService.CheckFontData();
        }
    }
}
