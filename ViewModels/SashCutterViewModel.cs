using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class SashCutterViewModel: ViewModelBase
{
    public static ObservableCollection<WhipFieldViewModel> WhipFieldsViewModel { get; set; }
    
    public SashCutterViewModel()
    {
        WhipFieldsViewModel = new ObservableCollection<WhipFieldViewModel>();

    }

    public void AddWhipFieldButton()
    {
        WhipFieldsViewModel.Add(new WhipFieldViewModel());
    }

    public void RemoveWhipFieldButton()
    {
        WhipFieldsViewModel?.Remove(WhipFieldsViewModel[^1]);
    }
    
    
}