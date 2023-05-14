using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OptiCUT.ViewModels;

namespace OptiCUT.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}