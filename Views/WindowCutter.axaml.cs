using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiCUT.Views;

public partial class WindowCutter : UserControl
{
    public WindowCutter()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}