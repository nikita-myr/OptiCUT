using System.Collections.ObjectModel;
using DynamicData;
using OptiCUT.Objects;

namespace OptiCUT.ViewModels;

public class WhipFieldViewModel : ViewModelBase
{
    public Whip Whip { get; set; }
    public ObservableCollection<Detail> Details { get; set; }

    public WhipFieldViewModel()
    {
        Whip = new Whip();
        Details = new ObservableCollection<Detail> {new Detail()};
    }

    public void AddDetailButton()
    {
        Details.Add(new Detail());
    }


    public void RemoveDetailButton()
    {
        if(Details.Count > 0) Details.Remove(Details[^1]);
    }
}