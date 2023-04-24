using System.Collections.ObjectModel;
using DynamicData;

namespace OptiCUT.ViewModels;

public class FrameCutterViewModel: ViewModelBase
{
    private ObservableCollection<WhipFieldViewModel> WhipFieldViewModels { get; set; }
    
    public FrameCutterViewModel()
    {
        WhipFieldViewModels = new ObservableCollection<WhipFieldViewModel>{new WhipFieldViewModel()};

    }

    public void AddWhipFieldButton()
    {
        WhipFieldViewModels.Add(new WhipFieldViewModel());
    }

    public void RemoveWhipFieldButton()
    {
        if(WhipFieldViewModels.Count > 0) WhipFieldViewModels.Remove(WhipFieldViewModels[^1]);
    }

    public void Cut()
    {
        OptiCUT.Excel.Excel.GenerateFrameCutExcel();
    }
    
}