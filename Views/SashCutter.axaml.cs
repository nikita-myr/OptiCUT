using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiCUT.Views;

public partial class SashCutter : UserControl
{
    public SashCutter()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}