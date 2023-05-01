using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Aspose.Cells;
using OptiCUT.Excel;
using OptiCUT.ViewModels;

namespace OptiCUT.Models.Excel;

public static class ExcelGenerator
{
    /// <summary>
    /// Generate Excel file with cut data 
    /// </summary>


    private static int _rawOffset;

    public static void GenerateExcelFile(string? saveDirectory, string fileName = "CutResult",
        string fileType = ".xlsx", bool makePdf = false)
    {
        Workbook workbook = new Workbook();

        ObservableCollection<WhipField> frameWhips = GetFrameWhips();
        ObservableCollection<WhipField> sashWhips = GetSashWhips();

        //Check is data exist, if true add to worksheet and data to file 
        if (frameWhips.Count != 0) GenerateFrameCutSheet(workbook, frameWhips,
            FrameCutterViewModel.isNeedVsk213, FrameCutterViewModel.isNeedVsk214);
        if (sashWhips.Count != 0) GenerateSashCutSheet(workbook, sashWhips);
        
        
        workbook.CalculateFormula();
        
        workbook.Save(saveDirectory + fileName + fileType);
        if (makePdf) workbook.Save(saveDirectory + fileName + ".pdf");

    }

    private static void GenerateFrameCutSheet(Workbook workbook, ObservableCollection<WhipField> whipFields , 
        bool isNeedVsk213 = false, bool isNeedVsk214 = false)
    {
        _rawOffset = 1;

        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = "Каркас";

        worksheet.Cells.Merge(0, 0, 1, 13);
        Cell headerCell = worksheet.Cells["A1"];
        worksheet.Cells["A1"].PutValue("Каркас");
        headerCell.SetStyle(CellSetups.SetFirstHeaderStyle(headerCell));
        _rawOffset++;

        worksheet.Cells.Merge(1, 0, 2, 2);
        worksheet.Cells["A2"].PutValue("Маркировка");
        worksheet.Cells["A2"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["A2"]));
        _rawOffset++;

        worksheet.Cells.Merge(1, 2, 1, 8);
        worksheet.Cells["C2"].PutValue("Размеры");
        worksheet.Cells["C2"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C2"]));

        worksheet.Cells.Merge(1, 10, 2, 1);
        worksheet.Cells["K2"].PutValue("Кол-во,\nшт");
        worksheet.Cells["K2"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K2"], fontSize: 10));

        worksheet.Cells["C3"].PutValue("L, мм");
        worksheet.Cells["C3"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["E3"].PutValue("уг 1-°");
        worksheet.Cells["E3"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["F3"].PutValue("уг 2-°");
        worksheet.Cells["F3"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C3"]));
        _rawOffset++;

        //Writing up details
        foreach (var whipField in whipFields)
        {
            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
            worksheet.Cells["A" + _rawOffset]
                .PutValue($"{whipField.Whip.Label}, {whipField.Whip.Color} (хлысь {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset]
                .SetStyle(
                    CellSetups.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
            _rawOffset++;

            int startRowOffset = _rawOffset;

            GetDetails(worksheet, whipField.Details);

            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 9);
            worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
            worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K{startRowOffset}:K{_rawOffset - 1})";
            worksheet.Cells["K" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
            _rawOffset++;


        }
        
        //Adding additional field if needed
        //TODO: make fields automatically counted. 
        if (isNeedVsk213) WriteVsk213Fields(worksheet);
        if (isNeedVsk214) WriteVsk214Fields(worksheet);

        //Header
        worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
        worksheet.Cells["A" + _rawOffset].PutValue("Каркас. Оптимизация распила");
        worksheet.Cells["A" + _rawOffset].SetStyle(CellSetups.SetFirstHeaderStyle(worksheet.Cells["A" + _rawOffset]));
        _rawOffset++;


        //Writing up cutter result
        foreach (var whipField in whipFields)
        {
            worksheet.Cells["K" + _rawOffset].PutValue("ост мм");
            worksheet.Cells["K" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));

            worksheet.Cells["L" + _rawOffset].PutValue("кол. хл.");
            worksheet.Cells["L" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["L" + _rawOffset]));


            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 10);
            worksheet.Cells["A" + _rawOffset]
                .PutValue($"{whipField.Whip.Label}, {whipField.Whip.Color} (хлыст {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset]
                .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true));
            _rawOffset++;


            GetWhipsFromCutter(_rawOffset, worksheet, whipField);

        }

        DrawBorders(workbook, worksheet, _rawOffset);

        worksheet.PageSetup.FitToPagesWide = 1;
        worksheet.PageSetup.PrintArea = $"A1:M{_rawOffset}";
    }
    
    private static void WriteVsk213Fields(Worksheet worksheet)
    {
        worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
        worksheet.Cells["A" + _rawOffset]
            .PutValue($"Соед. стоечный ВС-К-213, без");
        worksheet.Cells["A" + _rawOffset]
            .SetStyle(
                CellSetups.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
        _rawOffset++;

        int startRowOffset = _rawOffset;

        worksheet.Cells["C" + _rawOffset].PutValue(360);
        worksheet.Cells["C" + _rawOffset].SetStyle(
            CellSetups.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));

        worksheet.Cells["K" + _rawOffset].PutValue("??");
        worksheet.Cells["K" + _rawOffset].SetStyle(
            CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));

        worksheet.Cells["E" + _rawOffset].PutValue("90°");
        worksheet.Cells["E" + _rawOffset]
            .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));

        worksheet.Cells["F" + _rawOffset].PutValue("90°");
        worksheet.Cells["F" + _rawOffset]
            .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset]));
        _rawOffset++;

        worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 9);
        worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
        worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K{startRowOffset}:K{_rawOffset - 1})";
        worksheet.Cells["K" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
        _rawOffset++;
        
        
    }

    private static void WriteVsk214Fields(Worksheet worksheet)
    {
        worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
        worksheet.Cells["A" + _rawOffset]
            .PutValue($"Т-cоед. 20мм ВС-К-214, без");
        worksheet.Cells["A" + _rawOffset]
            .SetStyle(
                CellSetups.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
        _rawOffset++;

        int startRowOffset = _rawOffset;

        worksheet.Cells["C" + _rawOffset].PutValue(20);
        worksheet.Cells["C" + _rawOffset].SetStyle(
            CellSetups.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));

        worksheet.Cells["K" + _rawOffset].PutValue("??");
        worksheet.Cells["K" + _rawOffset].SetStyle(
            CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));

        worksheet.Cells["E" + _rawOffset].PutValue("90°");
        worksheet.Cells["E" + _rawOffset]
            .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));

        worksheet.Cells["F" + _rawOffset].PutValue("90°");
        worksheet.Cells["F" + _rawOffset]
            .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset]));
        _rawOffset++;

        worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 9);
        worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
        worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K{startRowOffset}:K{_rawOffset - 1})";
        worksheet.Cells["K" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
        _rawOffset++;
        
    }
    
    private static void GenerateSashCutSheet(Workbook workbook, ObservableCollection<WhipField> whipFields)
    {
        _rawOffset = 1;
        
        Worksheet worksheet = workbook.Worksheets[workbook.Worksheets.Add()];
        worksheet.Name = "Штапики";

        worksheet.Cells.Merge(0, 0, 1, 13);
        Cell headerCell = worksheet.Cells["A1"];
        worksheet.Cells["A1"].PutValue("Штапики");
        headerCell.SetStyle(CellSetups.SetFirstHeaderStyle(headerCell));
        _rawOffset++;

        worksheet.Cells.Merge(1, 0, 2, 2);
        worksheet.Cells["A2"].PutValue("Маркировка");
        worksheet.Cells["A2"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["A2"]));
        _rawOffset++;

        worksheet.Cells.Merge(1, 2, 1, 8);
        worksheet.Cells["C2"].PutValue("Размеры");
        worksheet.Cells["C2"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C2"]));

        worksheet.Cells.Merge(1, 10, 2, 1);
        worksheet.Cells["K2"].PutValue("Кол-во,\nшт");
        worksheet.Cells["K2"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K2"], fontSize: 10));

        worksheet.Cells["C3"].PutValue("L, мм");
        worksheet.Cells["C3"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["E3"].PutValue("уг 1-°");
        worksheet.Cells["E3"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["F3"].PutValue("уг 2-°");
        worksheet.Cells["F3"].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["C3"]));
        _rawOffset++;

        //Writing up details
        foreach (var whipField in whipFields)
        {
            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
            worksheet.Cells["A" + _rawOffset]
                .PutValue($"{whipField.Whip.Label}, {whipField.Whip.Color} (хлысь {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset]
                .SetStyle(
                    CellSetups.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
            _rawOffset++;

            int startRowOffset = _rawOffset;

            GetDetails(worksheet, whipField.Details);

            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 9);
            worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
            worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K{startRowOffset}:K{_rawOffset - 1})";
            worksheet.Cells["K" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
            _rawOffset++;


        }



        worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
        worksheet.Cells["A" + _rawOffset].PutValue("Штапики. Оптимизация распила");
        worksheet.Cells["A" + _rawOffset].SetStyle(CellSetups.SetFirstHeaderStyle(worksheet.Cells["A" + _rawOffset]));
        _rawOffset++;

        //Writing up cutter result
        foreach (var whipField in whipFields)
        {
            worksheet.Cells["K" + _rawOffset].PutValue("ост мм");
            worksheet.Cells["K" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));

            worksheet.Cells["L" + _rawOffset].PutValue("кол. хл.");
            worksheet.Cells["L" + _rawOffset].SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["L" + _rawOffset]));


            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 10);
            worksheet.Cells["A" + _rawOffset]
                .PutValue($"{whipField.Whip.Label}, {whipField.Whip.Color} (хлысь {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset]
                .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true));
            _rawOffset++;


            GetWhipsFromCutter(_rawOffset, worksheet, whipField);

        }
        
        DrawBorders(workbook, worksheet, _rawOffset);
        
        worksheet.PageSetup.FitToPagesWide = 1;
        worksheet.PageSetup.PrintArea = $"A1:M{_rawOffset}";
    }

    private static void DrawBorders(Workbook workbook, Worksheet worksheet, int rawOffset)
    {
        CellsColor cellsColor = workbook.CreateCellsColor();
        
        Range range = worksheet.Cells.CreateRange(0, 0, rawOffset, 13);
        
        range.SetInsideBorders(BorderType.Vertical, CellBorderType.Thin, cellsColor);
        range.SetInsideBorders(BorderType.Horizontal, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.TopBorder, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.LeftBorder, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.RightBorder, CellBorderType.Thin, cellsColor);
        
    } 
    
    private static void GetDetails(Worksheet worksheet, ObservableCollection<Detail> details)
    {
        for (int i = 0; i < details.Count; i++)
        {
            worksheet.Cells["C" + _rawOffset].PutValue(details[i].Lenght);
            worksheet.Cells["C" + _rawOffset].SetStyle(
                CellSetups.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));

            worksheet.Cells["K" + _rawOffset].PutValue(details[i].Amount);
            worksheet.Cells["K" + _rawOffset].SetStyle(
                CellSetups.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));

            worksheet.Cells["E" + _rawOffset].PutValue("90°");
            worksheet.Cells["E" + _rawOffset]
                .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));

            worksheet.Cells["F" + _rawOffset].PutValue("90°");
            worksheet.Cells["F" + _rawOffset]
                .SetStyle(CellSetups.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset]));
            _rawOffset++;
        }
    }

    private static void GetWhipsFromCutter(int rawOffset, Worksheet worksheet, WhipField whipField)
    {
        List<string> columnIndex = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L"
        };

        int columnOffsetIndex = 0;

        int startRawOffset = rawOffset;

        List<CuttedWhip> cutResult = Cutter.Cut(whipField.Details, whipField.Whip);

        foreach (CuttedWhip whip in cutResult)
        {
            foreach (float detail in whip.Details)
            {
                if (columnOffsetIndex < 10)
                {
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(detail);
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellSetups.SetBasicCellStyle(
                        worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
                    columnOffsetIndex++;
                }
                else if (columnOffsetIndex == 10)
                {
                    columnOffsetIndex = 0;
                    rawOffset++;
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(detail);
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellSetups.SetBasicCellStyle(
                        worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
                    columnOffsetIndex++;

                }
            }

            columnOffsetIndex = 10;
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(whip.LenghtRemains);
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellSetups.SetBasicCellStyle(
                worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));

            columnOffsetIndex = 11;
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(whip.Amount);
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellSetups.SetBasicCellStyle(
                worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
            columnOffsetIndex = 0;
            rawOffset++;
        }

        worksheet.Cells.Merge(rawOffset - 1, 0, 1, 10);
        worksheet.Cells["K" + rawOffset].PutValue("Итого:");
        worksheet.Cells["L" + rawOffset].Formula = $"=SUM(L{startRawOffset}:L{rawOffset - 1})";
        worksheet.Cells["L" + rawOffset].SetStyle(style: CellSetups.SetBasicCellStyle(worksheet.Cells["L" + rawOffset]));
        rawOffset++;
        _rawOffset = rawOffset;
    }

    private static ObservableCollection<WhipField> GetFrameWhips()
    {
        ObservableCollection<WhipField> result = FrameCutterViewModel.WhipFields;
        return result;
    }

    private static ObservableCollection<WhipField> GetSashWhips()
    {
        ObservableCollection<WhipField> result = SashCutterViewModel.WhipFields;
        return result;
    }
}