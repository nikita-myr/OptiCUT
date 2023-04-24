using System.Collections.ObjectModel;

namespace OptiCUT.ViewModels;

public class SashCutterViewModel: ViewModelBase
{
    private ObservableCollection<WhipFieldViewModel> WhipFieldsViewModel { get; set; }
    
    public SashCutterViewModel()
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

    public void Cut()
    {
        OptiCUT.Excel.Excel.GenerateFrameCutExcel(WhipFieldsViewModel);
    }
    
}