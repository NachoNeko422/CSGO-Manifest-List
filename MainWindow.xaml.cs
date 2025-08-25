using App1.MainFrame;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WinRT.Interop;
using Microsoft.UI.Xaml.Controls;


namespace App1
{
    public sealed partial class MainWindow : Window
    {
        private readonly string apiUrl = "https://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1?appid=730";
        private readonly HttpClient httpClient = new HttpClient();
        // 将 refreshTimer 字段声明为可为 null
        private Timer? refreshTimer;
        private AppWindow? appWindow;

        public MainWindow()
        {
            this.InitializeComponent();
            var hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            appWindow = AppWindow.GetFromWindowId(myWndId);
            var presenter = appWindow?.Presenter as OverlappedPresenter;
            appWindow?.Resize(new Windows.Graphics.SizeInt32(1000, 600));
            if (presenter != null)
            {
                presenter.IsResizable = false;
            }

            if (appWindow != null)
            {
                appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            MainFrame.Navigate(typeof(HomePage));
            
            //60秒人数刷新
            _ = RefreshPlayerCount();
            refreshTimer = new Timer(_ =>
            {
                _ = DispatcherQueue.TryEnqueue(async () =>
                {
                    await RefreshPlayerCount();
                });
            }, null, 60_000, 60_000); // 60 秒后开始，每 60 秒刷新
        }

        private void CS2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(CS2),"731.json");
        }

        private void CSGO_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(CS2),"732.json");
        }

        private void CMD_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(CMD));
        }

        private void NachoDayo_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(NachoDayo));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(HomePage));
        }

        private async Task RefreshPlayerCount()
        {
            try
            {
                string json = await httpClient.GetStringAsync(apiUrl);

                using JsonDocument doc = JsonDocument.Parse(json);
                int playerCount = doc.RootElement
                                     .GetProperty("response")
                                     .GetProperty("player_count")
                                     .GetInt32();

                string text = Convert.ToString(playerCount);

                // UI 更新必须通过 DispatcherQueue
                DispatcherQueue.TryEnqueue(() =>
                {
                    ResultBlock.Text = text;
                });
            }
            catch (Exception ex)
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    ResultBlock.Text = $"获取失败: {ex.Message}";
                });
            }
        }
    }  
}
