using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OptiCUT.ViewModels;

namespace OptiCUT.Views;

public partial class BorderCutView : UserControl
{
    public BorderCutView()
    {
        InitializeComponent();
        DataContext = new BorderCutterViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}