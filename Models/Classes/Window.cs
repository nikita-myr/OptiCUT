using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;

namespace OptiCUT.Models;

public class Window
{
    public string Label { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public int Amount { get; private set; }
    public Whip Whip { get; set; }
    public ObservableCollection<Detail> Details { get; private set; }
    public string OpenType { get; set; }
    public string OpenSide { get; set; }
    public bool IsNeedBrush { get; set; }
    public string Color { get; set; }


    public Window(string label, int width, int height, int amount, string openSide, 
                  string openType, bool isNeedBrush, string color, int glassDepth)
    {
        Label = label;
        Height = height;
        Width = width;
        Amount = amount;
        OpenSide = openSide;
        OpenType = openType;
        IsNeedBrush = isNeedBrush;
        Color = color;
    }
    
}