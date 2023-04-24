using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class BorderCutterViewModel: ViewModelBase
{
    private ObservableCollection<WhipFieldViewModel> WhipFieldsViewModel { get; set; }
    
    public BorderCutterViewModel()
    {
        WhipFieldsViewModel = new ObservableCollection<WhipFieldViewModel>{new WhipFieldViewModel()};

    }

    public void AddWhipFieldButton()
    {
        WhipFieldsViewModel.Add(new WhipFieldViewModel());
    }

    public void RemoveWhipFieldButton()
    {
        if(WhipFieldsViewModel.Count > 0) WhipFieldsViewModel.Remove(WhipFieldsViewModel[^1]);
    }
    
}