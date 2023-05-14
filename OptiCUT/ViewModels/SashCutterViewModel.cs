using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class SashCutterViewModel: ViewModelBase
{
    public static ObservableCollection<PositionField> PositionFields { get; set; }
    
    public SashCutterViewModel()
    {
        PositionFields = new ObservableCollection<PositionField>();
    }

    public void AddWhipFieldButton()
    {
        PositionFields.Add(new PositionField());
    }

    public void RemoveWhipFieldButton()
    {
        PositionFields?.Remove(PositionFields[^1]);
    }
    
    
}