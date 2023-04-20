namespace OptiCUT.Objects;

public class Detail
{
    public int Lenght { get; private set; }
    public int Amount { get; private set; }

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