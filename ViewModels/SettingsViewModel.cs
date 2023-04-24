namespace OptiCUT.ViewModels;

public class SettingsViewModel: ViewModelBase
{
    public SettingsViewModel()
    {
        
    }

    private void GenerateCutExcel()
    {
        Excel.GenerateExcelFile();
    }
    
}