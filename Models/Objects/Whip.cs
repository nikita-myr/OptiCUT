using System.Collections.Generic;

namespace OptiCUT.Objects;

public class Whip
{
    public string Label { get; set; }
    public string Color { get; set; }
    public int Lenght { get; set; }
    public int SawWidth { get; set; }
    public int MinLenghtRemain { get; set; }

    public Whip(){}


    public Whip(string label, string color, int lenght, int sawWidth, int minLenghtRemain)
    {
        Label = label;
        Color = color;
        Lenght = lenght;
        SawWidth = sawWidth;
        MinLenghtRemain = minLenghtRemain;
    }
}


public class CuttedWhip : Whip
{
    public List<int> Details { get; private set; }
    public int LenghtRemains { get; private set; }
    public int Amount { get; private set; }
    

    public CuttedWhip(string label, string color, int lenght, int sawWidth, int minLenghtRemain) : base(label, color, lenght, sawWidth, minLenghtRemain)
    {
        Details = new List<int>();
    }

    public CuttedWhip(Whip whip) : base(whip.Label, whip.Color, whip.Lenght, whip.SawWidth, whip.MinLenghtRemain)
    {
        Details = new List<int>();
        LenghtRemains = whip.Lenght;
        Amount = 1;
    }

    public bool CanAddDetail(int detail)
    {
        if (LenghtRemains > detail + MinLenghtRemain) return true;
        return false;

    }

    public void AddDetail(int detail)
    {
        Details.Add(detail);
        LenghtRemains -= detail + SawWidth;
    }

    public void DecreaseAmount(int value)
    {
        Amount -= value;
    }

    public void IncreaseAmount(int value)
    {
        Amount += value;
    }

    public void SetAmount(int value)
    {
        Amount = value;
    }

}