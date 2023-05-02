using System.Collections.ObjectModel;
using Aspose.Cells;
using OptiCUT.ViewModels;

namespace OptiCUT.Models.Excel;

public static class ExcelGenerator
{
    /// <summary>
    /// Generate Excel files with cut and specifications data 
    /// </summary>

    public static void GenerateCutterExcelFile(string? saveDirectory, string fileName = "CutResult",
        string fileType = ".xlsx", bool makePdf = false)
    {
        Workbook workbook = new Workbook();

        ObservableCollection<Position> frameWhips = GetFrameWhips();
        ObservableCollection<Position> sashWhips = GetSashWhips();
        ObservableCollection<Position> borderWhips = GetBordersWhips();

        //Check is data exist, if true add to worksheet and data to file 
        if (frameWhips.Count != 0) AvangardStyledExcel.FrameCutSheet.GenerateSheet(workbook, frameWhips,
            FrameCutterViewModel.isNeedVsk213, FrameCutterViewModel.isNeedVsk214);
        if (sashWhips.Count != 0) AvangardStyledExcel.SashCutSheet.GenerateSheet(workbook, sashWhips);
        if(borderWhips.Count != 0) AvangardStyledExcel.BordersCutSheet.GenerateSheet(workbook, borderWhips);
        
        
        workbook.CalculateFormula();
        
        workbook.Save(saveDirectory + fileName + fileType);
        if (makePdf) workbook.Save(saveDirectory + fileName + ".pdf");

    }

    public static void GenerateSpecificationFile(string? saveDirectory, string fileName = "cutResult",
        string fileType = ".xlsx", bool makePdf = false, string objectLabel="Объект", string constructionLabel = "В-x")
    {
        Workbook workbook = new Workbook();
        
        AvangardStyledExcel.VentilationsAndWindowsSpecification.GenerateSpecification(workbook, GetWindows(), GetVentilations(),
            objectLabel, constructionLabel);
        
        
        
        workbook.CalculateFormula();
        workbook.Save(saveDirectory + fileName + fileType);
        if (makePdf) workbook.Save(saveDirectory + fileName + ".pdf");
        
    }

    
    private static ObservableCollection<Position> GetFrameWhips()
    {
        ObservableCollection<Position> result = FrameCutterViewModel.WhipFields;
        return result;
    }

    private static ObservableCollection<Position> GetSashWhips()
    {
        ObservableCollection<Position> result = SashCutterViewModel.WhipFields;
        return result;
    }

    private static ObservableCollection<Position> GetBordersWhips()
    {
        ObservableCollection<Position> result = BorderCutterViewModel.Positions;
        return result;
    }

    private static ObservableCollection<Window> GetWindows()
    {
        ObservableCollection<Window> result = WindowsAndVentilationCutterViewModel.Windows;
        return result;
    }

    private static ObservableCollection<Ventilation> GetVentilations()
    {
        ObservableCollection<Ventilation> result = WindowsAndVentilationCutterViewModel.Ventilations;
        return result;
    }
}