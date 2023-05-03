using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class WindowsAndVentilationCutterViewModel: ViewModelBase
{
    public static ObservableCollection<WindowField> WindowsFields { get; set; }
    public static ObservableCollection<VentilationField> VentilationsFields { get; set; }

    public static WhipField WhipField { get; set; }


    public WindowsAndVentilationCutterViewModel()
    {
        WindowsFields = new ObservableCollection<WindowField>();
        VentilationsFields = new ObservableCollection<VentilationField>();
        WhipField = new WhipField();
    }

    
    private void AddVentilationButton()
    {
        VentilationsFields.Add(new VentilationField());
    }

    private void RemoveVentilationButton()
    {
        if (VentilationsFields.Count > 0) VentilationsFields.Remove(VentilationsFields[^1]);
    }

    private void AddWindowButton()
    {
        WindowsFields.Add(new WindowField());
    }

    private void RemoveWindowButton()
    {
        if(WindowsFields.Count > 0) WindowsFields.Remove(WindowsFields[^1]);
    }
    
}