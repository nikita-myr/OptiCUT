using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class BorderCutterViewModel: ViewModelBase
{
    public static ObservableCollection<PositionField>? PositionFields { get; set; }
    
    public BorderCutterViewModel()
    {
        PositionFields = new ObservableCollection<PositionField>();
    }

    public void AddWhipFieldButton()
    {
        PositionFields?.Add(new PositionField());
    }

    public void RemoveWhipFieldButton()
    {
        if(PositionFields.Count > 0) PositionFields.Remove(PositionFields[^1]);
    }
    
}