using System.Collections.ObjectModel;

namespace OptiCUT.Models;

public enum OpenType
{
    LeftTurn,
    RightTurn,
    LeftTurnBrush,
    RightTurnBrush,
    LeftTiltTurn,
    RightTiltTurn,
}

public class Window
{
    public string Label { get; set; }
    public int Amount { get; set; }
    public ObservableCollection<Detail> Details { get; set; }
    public OpenType OpenType { get; set; }
    
    

    public Window(string label, int amount, ObservableCollection<Detail> details, OpenType openType)
    {
        Label = label;
        Amount = amount;
        Details = details;
        OpenType = openType;
    }

}