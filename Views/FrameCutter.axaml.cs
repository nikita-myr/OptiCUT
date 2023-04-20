using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiCUT.Views;

public partial class FrameCutter : UserControl
{
    public FrameCutter()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}