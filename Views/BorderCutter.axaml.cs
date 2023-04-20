using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiCUT.Views;

public partial class BorderCutter : UserControl
{
    public BorderCutter()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}