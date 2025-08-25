using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.MainFrame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CMD : Page
    {
        public CMD()
        {
            InitializeComponent();
            Input731.TextChanged += Input731_TextChanged;
            Input732.TextChanged += Input732_TextChanged;
        }

        public static class CMDState
        {
            public static string Input731 { get; set; } = string.Empty;
            public static string Input732 { get; set; } = string.Empty;
            public static bool HasRun { get; set; } = false; // 标记用户是否点击过GO
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // 恢复输入
            Input731.Text = CMDState.Input731;
            Input732.Text = CMDState.Input732;

            // 如果用户之前点击过GO，则恢复输出和显示控件
            if (CMDState.HasRun)
            {
                Output731.Text = $"download_depot 730 731 {CMDState.Input731}";
                Output732.Text = $"download_depot 730 731 {CMDState.Input732}";
                CMDCC.Visibility = Visibility.Visible;
                Output731.Visibility = Visibility.Visible;
                Output732.Visibility = Visibility.Visible;
                SteamCMD.Visibility = Visibility.Visible;
                WinR.Visibility = Visibility.Visible;
            }
        }

        private void GO_Click(object sender, RoutedEventArgs e)
        {
            string userInput731 = Input731.Text;
            string userInput732 = Input732.Text;

            if (!string.IsNullOrWhiteSpace(userInput731) && !string.IsNullOrWhiteSpace(userInput731))
            {
                CMDState.Input731 = userInput731;
                CMDState.Input732 = userInput732;
                CMDState.HasRun = true;

                Output731.Text = $"download_depot 730 731 {userInput731}";
                Output732.Text = $"download_depot 730 731 {userInput732}";
                CMDCC.Visibility = Visibility.Visible;
                Output731.Visibility = Visibility.Visible;
                Output732.Visibility = Visibility.Visible;
                SteamCMD.Visibility = Visibility.Visible;
                WinR.Visibility = Visibility.Visible;
            }
        }
        private void Input731_TextChanged(object sender, TextChangedEventArgs e)
        {
            CMDState.Input731 = Input731.Text;
        }

        private void Input732_TextChanged(object sender, TextChangedEventArgs e)
        {
            CMDState.Input732 = Input732.Text;
        }
    }
}
