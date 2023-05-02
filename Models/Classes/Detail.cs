using OptiCUT.ViewModels;

namespace OptiCUT.Models;

public class Detail
{
    public int Lenght { get; set; }
    public int Amount { get; set; }

    public Detail()
    {

    }

    public Detail(Detail detail)
    {
        Lenght = detail.Lenght;
        Amount = detail.Amount;
    }
    
    public Detail(int lenght, int amount)
    {
        Lenght = lenght;
        Amount = amount;
    }
    
    public Detail(DetailField detailField)
    {
        Lenght = detailField.Lenght;
        Amount = detailField.Amount;
    }


    public void DecreaseDetailAmount(int value)
    {
        Amount -= value;
    }

    public void IncreaseDetailAmount(int value)
    {
        Amount += value;
    }
}