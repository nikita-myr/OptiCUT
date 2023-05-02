using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class BorderCutterViewModel: ViewModelBase
{
    public static ObservableCollection<Position> Positions { get; set; }
    
    public BorderCutterViewModel()
    {
        Positions = new ObservableCollection<Position>();

    }

    public void AddWhipFieldButton()
    {
        Positions.Add(new Position());
    }

    public void RemoveWhipFieldButton()
    {
        if(Positions.Count > 0) Positions.Remove(Positions[^1]);
    }
    
}