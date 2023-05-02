namespace OptiCUT.ViewModels;

public class WhipField
{
    public string Label { get; set; }
    public string Color { get; set; }
    public int Lenght { get; set; }
    public int SawWidth { get; set; }
    public int MinLenghtRemain { get; set; }
    
    public WhipField() {}

    public WhipField(string label, string color, int lenght, int sawWidth, int minLenghtRemain)
    {
        Label = label;
        Color = color;
        Lenght = lenght;
        SawWidth = sawWidth;
        MinLenghtRemain = minLenghtRemain;
    }
}