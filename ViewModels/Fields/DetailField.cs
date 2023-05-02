using Avalonia.Controls;

namespace OptiCUT.ViewModels;

public class DetailField
{
    public int Lenght { get; set; }
    public int Amount { get; set; }
    
    public DetailField(){}

    public DetailField(int lenght, int amount)
    {
        Lenght = lenght;
        Amount = amount;
    }
    
}