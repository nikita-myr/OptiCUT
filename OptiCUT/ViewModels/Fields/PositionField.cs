using System.Collections.ObjectModel;
using OptiCUT.Models;

namespace OptiCUT.ViewModels;

public class PositionField
{
    public WhipField WhipField  { get; set; }
    public ObservableCollection<DetailField> DetailsField { get; set; }
    public PositionField()
    {
        WhipField = new WhipField();
        DetailsField = new ObservableCollection<DetailField>();
    }
    
    public void AddDetailButton()
    {
        DetailsField.Add(new DetailField());
    }


    public void RemoveDetailButton()
    {
        if(DetailsField.Count > 0) DetailsField.Remove(DetailsField[^1]);
    }
}