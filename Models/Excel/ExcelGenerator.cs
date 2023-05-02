using System.Collections.ObjectModel;
using Aspose.Cells;
using DynamicData;
using OptiCUT.Models.Excel.AvangardStyledExcel;
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
        ObservableCollection<Position> windowWhips = GetWindowsWhips();
        ObservableCollection<Position> ventilationWhips = GetVentilationsWhips();

        //Check is data exist, if true add to worksheet and data to file 
        if (frameWhips.Count != 0) FrameCutSheet.GenerateSheet(workbook, frameWhips,
            FrameCutterViewModel.isNeedVsk213, FrameCutterViewModel.isNeedVsk214);
        if (sashWhips.Count != 0) SashCutSheet.GenerateSheet(workbook, sashWhips);
        if(borderWhips.Count != 0) BordersCutSheet.GenerateSheet(workbook, borderWhips);
        if(ventilationWhips.Count != 0) VentilationCutSheet.GenerateSheet(workbook, ventilationWhips);
        if (windowWhips.Count != 0) WindowsCutSheet.GenerateSheet(workbook, windowWhips);


            workbook.CalculateFormula();
        
        workbook.Save(saveDirectory + fileName + fileType);
        if (makePdf) workbook.Save(saveDirectory + fileName + ".pdf");

    }

    public static void GenerateSpecificationFile(string? saveDirectory, string fileName = "cutResult",
        string fileType = ".xlsx", bool makePdf = false, string objectLabel="Объект", string constructionLabel = "В-x")
    {
        Workbook workbook = new Workbook();
        
        VentilationsAndWindowsSpecification.GenerateSpecification(workbook, GetWindows(), GetVentilations(),
            objectLabel, constructionLabel);
        
        
        
        workbook.CalculateFormula();
        workbook.Save(saveDirectory + fileName + fileType);
        if (makePdf) workbook.Save(saveDirectory + fileName + ".pdf");
        
    }

    
    private static ObservableCollection<Position> GetFrameWhips()
    {
        ObservableCollection<PositionField> positionFields = FrameCutterViewModel.PositionFields;

        ObservableCollection<Position> result = new ObservableCollection<Position>();
        foreach (PositionField positionField in positionFields)
        {
            result.Add(new Position(positionField.WhipField, positionField.DetailsField));
        }
        return result;
    }

    private static ObservableCollection<Position> GetSashWhips()
    {
        ObservableCollection<PositionField> positionFields = SashCutterViewModel.PositionFields;

        ObservableCollection<Position> result = new ObservableCollection<Position>();

        foreach (PositionField positionField in positionFields)
        {
            result.Add(new Position(positionField.WhipField, positionField.DetailsField));
        }

        return result;
    }

    private static ObservableCollection<Position> GetBordersWhips()
    {
        ObservableCollection<PositionField> positionFields = BorderCutterViewModel.PositionFields;
        
        ObservableCollection<Position> result = new ObservableCollection<Position>();
        
        foreach (PositionField positionField in positionFields)
        {
            result.Add(new Position(positionField.WhipField, positionField.DetailsField));
        }
        
        return result;
    }

    private static ObservableCollection<Window> GetWindows()
    {
        ObservableCollection<WindowField> windowsFields = WindowsAndVentilationCutterViewModel.WindowsFields;

        ObservableCollection<Window> result = new ObservableCollection<Window>();
        foreach (WindowField windowField in windowsFields)
        {
            result.Add(new Window(windowField));
        }
        return result;
    }

    private static ObservableCollection<Ventilation> GetVentilations()
    {
        ObservableCollection<VentilationField> ventilationFields = WindowsAndVentilationCutterViewModel.VentilationsFields;

        ObservableCollection<Ventilation> result = new ObservableCollection<Ventilation>();
        foreach (VentilationField ventilationField in ventilationFields)
        {
            result.Add(new Ventilation(ventilationField));
        }
        return result;
    }

    private static ObservableCollection<Position> GetWindowsWhips()
    {
        ObservableCollection<Position> result = new ObservableCollection<Position>();
        ObservableCollection<Window> windows = GetWindows();
        ObservableCollection<Detail> details = new ObservableCollection<Detail>();
        foreach (var window in windows)
        {
            details.Add(new Detail(window.Height, window.Amount * 2));
            details.Add(new Detail(window.Width, window.Amount * 2));
        }

        result.Add(new Position(WindowsAndVentilationCutterViewModel.WhipField, details));
        
        return result;
    }

    private static ObservableCollection<Position> GetVentilationsWhips()
    {
        ObservableCollection<Position> result = new ObservableCollection<Position>();
        ObservableCollection<Ventilation> ventilations = GetVentilations();
        ObservableCollection<Detail> vspDetails = new ObservableCollection<Detail>();
        ObservableCollection<Detail> vsm086Details = new ObservableCollection<Detail>();
        ObservableCollection<Detail> vsm085Details = new ObservableCollection<Detail>();
        foreach (var ventilation in ventilations)
        {
            vspDetails.Add(new Detail(ventilation.Height-63, ventilation.Amount*2));
            vspDetails.Add(new Detail(ventilation.Width-33, ventilation.Amount*2));
            vsm085Details.Add(new Detail(ventilation.Height-77, ventilation.Amount*2));
            vsm085Details.Add(new Detail(ventilation.Width-43, ventilation.Amount*2));
            vsm086Details.Add(new Detail(ventilation.Width-77, ventilation.Amount*3));
        }
        
        result.Add(new Position(new Whip("ВСП-217",ventilations[0].Color, 6100, 5, 1), vspDetails));
        result.Add(new Position(new Whip("ВС-М-085", ventilations[0].Color, 6100, 5, 1), vsm085Details));
        result.Add(new Position(new Whip("ВС-М-086", ventilations[0].Color, 6100, 5, 1), vsm086Details));
        
        return result;
    }
}