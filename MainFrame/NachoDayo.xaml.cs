using Microsoft.UI.Xaml.Controls;
using System.Threading;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.MainFrame;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NachoDayo : Page
{
    private Timer? refreshTimer;
    public NachoDayo()
    {
        InitializeComponent();
        refreshTimer?.Dispose();
        refreshTimer = null;
    }
}
