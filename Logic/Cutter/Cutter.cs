using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OptiCUT.Objects;

namespace OptiCUT.Logic;

public class Cutter
{
    public static List<CuttedWhip> Cut(ObservableCollection<Detail> details, Whip whip)
    {
        List<CuttedWhip> result = new List<CuttedWhip>();

        List<Detail> sortedDetails = SortByLenght(details);
        CuttedWhip cuttedWhip;


        foreach (var detail in sortedDetails)
        {
            int detailsAmount = GetDetailsAmount(sortedDetails);

            cuttedWhip = new CuttedWhip(whip);

            for (int i = 0; i <= detailsAmount;)
            {
                if (cuttedWhip.CanAddDetail(detail.Lenght) && detail.Amount > 0)
                {
                    cuttedWhip.AddDetail(detail.Lenght);
                    detail.DecreaseDetailAmount(1);
                }
                else
                {
                    if (cuttedWhip.LenghtRemains >= cuttedWhip.MinLenghtRemain)
                    {
                        foreach (var tailDetail in sortedDetails)
                        {
                            int tailDetailsAmount = GetDetailsAmount(sortedDetails);
                            for (int j = 0; j <= tailDetailsAmount; j++)
                            {
                                if (tailDetail.Amount > 0 && tailDetail.Lenght <= cuttedWhip.LenghtRemains - cuttedWhip.MinLenghtRemain)
                                {
                                    cuttedWhip.AddDetail(tailDetail.Lenght);
                                    tailDetail.DecreaseDetailAmount(1);
                                }
                            }
                        }
                    }
                    if (cuttedWhip.Details.Count > 0)
                    {
                        result.Add(cuttedWhip);
                        cuttedWhip = new CuttedWhip(whip);
                    }
                    i++;
                }
            }
        }
        return CheckDuplicates(result);
    }


    private static List<CuttedWhip> CheckDuplicates(List<CuttedWhip> cuttedWhips)
    {
        List<CuttedWhip> result = new List<CuttedWhip>();
        CuttedWhip currentWhip;
        int count;

        for (int i = 0; i < cuttedWhips.Count; i++)
        {
            count = 0;
            currentWhip = cuttedWhips[i];
            foreach (var whip in cuttedWhips)
            {
                //TODO: данный if вызывает сомнения надо бы перепроверить а точнее, как считает count
                if (CheckAreEqual(whip, currentWhip))
                {
                    count++;
                }
            }
            currentWhip.SetAmount(count);
            if (!CheckAreExist(result, currentWhip))
            {
                result.Add(currentWhip);
            }
        }
        return result;
    }

    private static bool CheckAreEqual(CuttedWhip firstWhip, CuttedWhip secondWhip)
    {
        if (firstWhip.Details.Count() == secondWhip.Details.Count())
        {
            for (int j = 0; j < secondWhip.Details.Count; j++)
            {
                if (firstWhip.Details[j] == secondWhip.Details[j] && firstWhip.LenghtRemains == secondWhip.LenghtRemains)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool CheckAreExist(List<CuttedWhip> cuttedWhips, CuttedWhip whip)
    {
        for (int i = 0; i < cuttedWhips.Count; i++)
        {
            if (cuttedWhips[i].Details.Count() == whip.Details.Count())
            {
                for (int j = 0; j < whip.Details.Count; j++)
                {
                    if (cuttedWhips[i].Details[j] == whip.Details[j] && cuttedWhips[i].LenghtRemains == whip.LenghtRemains)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private static int GetDetailsAmount(List<Detail> details)
    {
        int result = 0;
        foreach (var detail in details)
        {
            result += detail.Amount;
        }
        return result;
    }


    private static List<Detail> SortByLenght(ObservableCollection<Detail> details)
    {
        var sortedDetails = details.OrderByDescending(detail => detail.Lenght);

        List<Detail> result = new List<Detail>();

        foreach (Detail detail in sortedDetails)
        {
            result.Add(detail);
        }

        return result;
    }
}