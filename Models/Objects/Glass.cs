namespace OptiCUT.Excel.Objects;

public class Glass
{
    public int Height { get; private set; }
    public int Width { get; private set; }
    public int Depth { get; private set; }
    public string Label { get; private set; }
    public int Square { get; private set; }

    public Glass(int width, int height, int depth, string label)
    {
        Width = width;
        Height = height;
        Depth = depth;
        Label = label;
        Square = width * height;
    }
}