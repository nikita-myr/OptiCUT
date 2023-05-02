using System.Collections.ObjectModel;
using OptiCUT.ViewModels;

namespace OptiCUT.Models;

public class Ventilation
{
    public string Label { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Amount { get; set; }
    public string Color { get; set; }

    public Ventilation() { }

    public Ventilation(string label, int width, int height, int amount, string color)
    {
        Label = label;
        Width = width;
        Height = height;
        Amount = amount;
        Color = color;
    }
    
}