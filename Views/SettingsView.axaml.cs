using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiCUT.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        DataContext = new ViewModels.SettingsViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}