using System.Collections.ObjectModel;
using System.Security.AccessControl;
using Aspose.Cells;
using Aspose.Cells.Drawing;

namespace OptiCUT.Excel.Objects;

public class Window
{
    private string _label;
    private int _amount;
    private ObservableCollection<Detail> _details;

    public enum openType
    {
        LeftTurn,
        RightTurn,
        LeftTurnBrush,
        RightTurnBrush,
        LeftTiltTurn,
        RightTiltTurn,
    }

    public string Label { get; set; }
    public int Amount { get; set; }
    public ObservableCollection<Detail> Details { get; set; }
    public openType OpenType { get; set; }
    
    


    Window(string label, int amount, ObservableCollection<Detail> details, openType openType)
    {
        Label = label;
        Amount = amount;
        Details = details;
        OpenType = openType;
    }

}