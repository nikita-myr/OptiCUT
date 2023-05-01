namespace OptiCUT.Models;

public class Detail
{
    public int Lenght { get; set; }
    public int Amount { get; set; }

    public Detail()
    {

    }

    public Detail(int lenght, int amount)
    {
        Lenght = lenght;
        Amount = amount;
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