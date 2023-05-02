using System.Collections.ObjectModel;
using DynamicData;
using OptiCUT.Models;
using OptiCUT.ViewModels;

namespace OptiCUT.Models;

public class Position
{
    public Whip Whip { get; set; }
    public ObservableCollection<Detail> Details { get; set; }

    public Position(WhipField whipField, ObservableCollection<DetailField> detailFields)
    {
        Whip = new Whip(whipField);
        Details = new ObservableCollection<Detail>();
        foreach(DetailField detailField in detailFields)
        {
            Details.Add(new Detail(detailField));
        }
    }
    
    public Position(WhipField whipField, ObservableCollection<Detail> details)
    {
        Whip = new Whip(whipField);
        Details = new ObservableCollection<Detail>();
        foreach(Detail detail in details)
        {
            Details.Add(new Detail(detail));
        }
    }

    public Position(Whip whip, ObservableCollection<Detail> details)
    {
        Whip = new Whip(whip);
        Details = new ObservableCollection<Detail>();
        foreach (Detail detail in details)
        {
            Details.Add(new Detail(detail));
        }
    }

    
}