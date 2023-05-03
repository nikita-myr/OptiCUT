using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using OptiCUT.ViewModels;

namespace OptiCUT.Models;

public class Window
{
    public string Label { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public int Amount { get; private set; }
    public Glass Glass { get; private set; }
    
    public string OpenType { get; private set; }
    public string OpenSide { get; private set; }
    public bool IsNeedBrush { get; private set; }
    public string Color { get; private set; }


    public Window(WindowField windowField)
    {
        Label = windowField.Label;
        Height = windowField.Height;
        Width = windowField.Width;
        Amount = windowField.Amount;
        OpenSide = windowField.OpenSide;
        OpenType = windowField.OpenType;
        IsNeedBrush = windowField.IsNeedBrush;
        Color = windowField.Color;
        Glass = new Glass(windowField.Width-43, windowField.Height-43, windowField.GlassDepth, windowField.GlassLabel);
    }
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
        Glass = new Glass(width-43, height-43, glassDepth, label);
    }

}