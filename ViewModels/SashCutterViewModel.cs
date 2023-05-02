using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class SashCutterViewModel: ViewModelBase
{
    public static ObservableCollection<Position> WhipFields { get; set; }
    
    public SashCutterViewModel()
    {
        WhipFields = new ObservableCollection<Position>();

    }

    public void AddWhipFieldButton()
    {
        WhipFields.Add(new Position());
    }

    public void RemoveWhipFieldButton()
    {
        WhipFields?.Remove(WhipFields[^1]);
    }
    
    
}