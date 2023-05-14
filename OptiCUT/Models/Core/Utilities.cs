using System.Collections.ObjectModel;

namespace OptiCUT.Models.Core;

public static class Utilities
{
    
    //TODO: made structured list of details
    public static ObservableCollection<Detail> GetStackedDetails(ObservableCollection<Detail> details)
    {
        ObservableCollection<Detail> result = new ObservableCollection<Detail>();
        
        Detail bufferDetail = new Detail();

        for (int i = 0; i < details.Count; i++)
        {
            bufferDetail = details[i];
            

        }
            return result;
    }
}