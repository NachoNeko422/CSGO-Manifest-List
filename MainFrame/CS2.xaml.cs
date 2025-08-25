using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace App1.MainFrame;

public class ManifestEntry
{
    public string ManifestId { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
}

public sealed partial class CS2 : Page
{
    private List<ManifestEntry>? manifests;
    private string FilePath = string.Empty;
    private Timer? refreshTimer;

    public CS2()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        

        if (e.Parameter is string temp)
        {
            FilePath = temp;
            _ = LoadManifestAsync();
        }
    }

    private async Task LoadManifestAsync()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "src", "manifest", FilePath);

        if (!File.Exists(filePath))
        {
            manifests = new List<ManifestEntry>
        {
            new ManifestEntry { ManifestId = "", Date = "未找到 manifest.json 文件" }
        };
        }
        else
        {
            string json = await File.ReadAllTextAsync(filePath);
            manifests = JsonSerializer.Deserialize<List<ManifestEntry>>(json) ?? new List<ManifestEntry>();

            if (manifests.Count == 0)
            {
                manifests = new List<ManifestEntry>
            {
                new ManifestEntry { ManifestId = "", Date = "没有可显示的 manifest" }
            };
            }
        }

        ManifestList.ItemsSource = manifests;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        manifests = null;
        ManifestList.ItemsSource = null;
        ManifestList.Items.Clear();
        refreshTimer?.Dispose();
        refreshTimer = null;
        this.Content = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
