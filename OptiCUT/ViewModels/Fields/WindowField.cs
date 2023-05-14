using System.Collections.ObjectModel;
using OptiCUT.Models;

namespace OptiCUT.ViewModels;

public class WindowField
{
    public string Label { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Amount { get; set; }
    public string OpenType { get; set; }
    public string OpenSide { get; set; }
    public bool IsNeedBrush { get; set; }
    public string Color { get; set; }
    
    public int GlassDepth { get; set; }
    public string GlassLabel { get; set; }
    
    public WindowField(){}
    
}