using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OptiCUT.ViewModels;

namespace OptiCUT.Views;

public partial class FrameCutView : UserControl
{
    public FrameCutView()
    {
        InitializeComponent();
        DataContext = new FrameCutterViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}