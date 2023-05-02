using System.Collections.ObjectModel;
using ReactiveUI;

namespace OptiCUT.ViewModels;

public class FrameCutterViewModel: ViewModelBase
{
    public static bool isNeedVsk213;
    public static int vsk213Amount;
    public static bool isNeedVsk214;
    public static int vsk214Amount;
    public static ObservableCollection<Position> WhipFields { get; set; }

    public bool IsNeedVsk213
    {
        get => isNeedVsk213;
        set => this.RaiseAndSetIfChanged(ref isNeedVsk213, value);
    }

    public int Vsk213Amount
    {
        get => vsk213Amount;
        set => this.RaiseAndSetIfChanged(ref vsk213Amount, value);
    }

    public bool IsNeedVsk214
    {
        get => isNeedVsk214;
        set => this.RaiseAndSetIfChanged(ref isNeedVsk214, value);
    }

    public int Vsk214Amount
    {
        get => vsk214Amount;
        set => this.RaiseAndSetIfChanged(ref vsk214Amount, value);
    }
    
    public FrameCutterViewModel()
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