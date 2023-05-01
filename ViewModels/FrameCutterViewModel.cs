using System.Collections.ObjectModel;
using ReactiveUI;

namespace OptiCUT.ViewModels;

public class FrameCutterViewModel: ViewModelBase
{
    public static bool isNeedVsk213;
    public static bool isNeedVsk214;
    public static ObservableCollection<WhipField> WhipFields { get; set; }

    public bool IsNeedVsk213
    {
        get => isNeedVsk213;
        set => this.RaiseAndSetIfChanged(ref isNeedVsk213, value);
    }

    public bool IsNeedVsk214
    {
        get => isNeedVsk214;
        set => this.RaiseAndSetIfChanged(ref isNeedVsk214, value);
    }
    
    public FrameCutterViewModel()
    {
        WhipFields = new ObservableCollection<WhipField>();
    }

    public void AddWhipFieldButton()
    {
        WhipFields.Add(new WhipField());
    }

    public void RemoveWhipFieldButton()
    {
        WhipFields?.Remove(WhipFields[^1]);
    }
    
    
}