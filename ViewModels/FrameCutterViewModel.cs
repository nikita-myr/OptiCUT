using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class FrameCutterViewModel: ViewModelBase
{
    public static ObservableCollection<WhipFieldViewModel> WhipFieldsViewModel { get; set; }
    
    public FrameCutterViewModel()
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