using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OptiCUT.ViewModels;

namespace OptiCUT.Views;

public partial class SashCutterView : UserControl
{
    public SashCutterView()
    {
        InitializeComponent();
        DataContext = new SashCutterViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}