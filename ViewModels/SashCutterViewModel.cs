using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class SashCutterViewModel: ViewModelBase
{
    public static ObservableCollection<WhipField> WhipFields { get; set; }
    
    public SashCutterViewModel()
    {
        WhipFields = new ObservableCollection<WhipField>();

    }

    public void AddWhipFieldButton()
    {
        WhipFields.Add(new WhipField());
    }

    public void RemoveWhipFieldButton()
    {
        WhipFields?.Remove(WhipFields[^1]);
    }
    
    
}