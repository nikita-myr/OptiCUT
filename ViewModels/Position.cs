using System.Collections.ObjectModel;
using OptiCUT.Models;

namespace OptiCUT.ViewModels;

public class Position
{
    public Whip Whip { get; set; }
    public ObservableCollection<Detail> Details { get; set; }

    public Position()
    {
        Whip = new Whip();
        Details = new ObservableCollection<Detail>();
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