using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class BorderCutterViewModel: ViewModelBase
{
    private ObservableCollection<WhipField> WhipFields { get; set; }
    
    public BorderCutterViewModel()
    {
        WhipFields = new ObservableCollection<WhipField>{new WhipField()};

    }

    public void AddWhipFieldButton()
    {
        WhipFields.Add(new WhipField());
    }

    public void RemoveWhipFieldButton()
    {
        if(WhipFields.Count > 0) WhipFields.Remove(WhipFields[^1]);
    }
    
}