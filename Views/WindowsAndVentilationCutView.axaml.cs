using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OptiCUT.ViewModels;

namespace OptiCUT.Views;

public partial class WindowsAndVentilationCutView : UserControl
{
    public WindowsAndVentilationCutView()
    {
        InitializeComponent();
        DataContext = new WindowsAndVentilationCutterViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}