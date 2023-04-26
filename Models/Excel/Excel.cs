using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Aspose.Cells;
using OptiCUT.Objects;
using OptiCUT.ViewModels;

namespace OptiCUT;

public static class Excel
{
    /// <summary>
    /// Generate Excel file with cut data 
    /// </summary>
    
    
    private static int _rawOffset;

    public static void GenerateExcelFile(string? saveDirectory, string fileName = "CutResult", string fileType = ".xlsx", bool makePdf = false)
    {
        Workbook workbook = new Workbook();

        ObservableCollection<WhipFieldViewModel> frameWhips = GetFrameWhips();
        ObservableCollection<WhipFieldViewModel> sashWhips = GetSashWhips();

        //Check is data exist, if true add to worksheet and data to file 
        if(frameWhips.Count > 0) GenerateFrameCutSheet(workbook, frameWhips);
        if(sashWhips.Count > 0) GenerateSashCutSheet(workbook, sashWhips);
        
        

        //TODO: PDF looks really bad, need to fix printBorders.
        if (makePdf) workbook.Save(saveDirectory + fileName + ".pdf");
        workbook.Save(saveDirectory + fileName + fileType);
        
    }

    

    private static void GenerateFrameCutSheet(Workbook workbook, ObservableCollection<WhipFieldViewModel> whipFields)
    {
        _rawOffset = 1;

        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = "Каркас";
        
        worksheet.Cells.Merge(0,0,1,13);
        Cell headerCell = worksheet.Cells["A1"];
        worksheet.Cells["A1"].PutValue("Каркас");
        headerCell.SetStyle(SetFirstHeaderStyle(headerCell));
        _rawOffset++;
        
        worksheet.Cells.Merge(1,0,2,2);
        worksheet.Cells["A2"].PutValue("Маркировка");
        worksheet.Cells["A2"].SetStyle(SetBasicCellStyle(worksheet.Cells["A2"]));
        _rawOffset++;
        
        worksheet.Cells.Merge(1,2,1,8);
        worksheet.Cells["C2"].PutValue("Размеры");
        worksheet.Cells["C2"].SetStyle(SetBasicCellStyle(worksheet.Cells["C2"]));
        
        worksheet.Cells.Merge(1,10,2,1);
        worksheet.Cells["K2"].PutValue("Кол-во,\nшт");
        worksheet.Cells["K2"].SetStyle(SetBasicCellStyle(worksheet.Cells["K2"], fontSize: 10));
        
        worksheet.Cells["C3"].PutValue("L, мм");
        worksheet.Cells["C3"].SetStyle(SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["E3"].PutValue("уг 1-°");
        worksheet.Cells["E3"].SetStyle(SetBasicCellStyle(worksheet.Cells["C3"]));
        
        worksheet.Cells["F3"].PutValue("уг 2-°");
        worksheet.Cells["F3"].SetStyle(SetBasicCellStyle(worksheet.Cells["C3"]));
        _rawOffset++;

        //Writing up details
        foreach (var whipField in whipFields)
        {
            worksheet.Cells.Merge(_rawOffset-1,0,1,13);
            worksheet.Cells["A" + _rawOffset].PutValue($"Стойка {whipField.Whip.Label}, {whipField.Whip.Color} (хлысь {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
            _rawOffset++;
            
            int startRowOffset = _rawOffset;
        
            GetDetails(worksheet , whipField.Details);
            
            worksheet.Cells.Merge(_rawOffset-1,0,1,9);
            worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
            worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K{startRowOffset}:K{_rawOffset - 1})";
            worksheet.Cells["K" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
            _rawOffset++;
            
            
        }
        
        worksheet.Cells.Merge(_rawOffset-1,0,1,13);
        worksheet.Cells["A" + _rawOffset].PutValue($"Соед. стоечный ВСК-213, без)");
        worksheet.Cells["A" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
        _rawOffset++;
        
        
        
        
        
        
        
        //Header
        worksheet.Cells.Merge(_rawOffset-1,0,1,13);
        worksheet.Cells["A" + _rawOffset].PutValue("Каркас. Оптимизация распила");
        worksheet.Cells["A"+_rawOffset].SetStyle(SetFirstHeaderStyle(worksheet.Cells["A" + _rawOffset]));
        _rawOffset++;

        
        //Writing up cutter result
        foreach (var whipField in whipFields)
        {
            worksheet.Cells["K" + _rawOffset].PutValue("ост мм");
            worksheet.Cells["K" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["K"+_rawOffset]));
        
            worksheet.Cells["L" + _rawOffset].PutValue("кол. хл.");
            worksheet.Cells["L" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["L"+_rawOffset]));
        
        
            worksheet.Cells.Merge(_rawOffset-1,0,1,10);
            worksheet.Cells["A" + _rawOffset].PutValue($"Стойка {whipField.Whip.Label}, {whipField.Whip.Color} (хлысь {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["A"+_rawOffset], isBold: true));
            _rawOffset++;


            GetWhipsFromCutter(_rawOffset, worksheet, whipField);
            
        }
    }

    private static void GenerateSashCutSheet(Workbook workbook, ObservableCollection<WhipFieldViewModel> whipFields)
    {
        _rawOffset = 1;

        int i = workbook.Worksheets.Add();
        
        Worksheet worksheet = workbook.Worksheets[i];
        worksheet.Name = "Штапики";
        
                worksheet.Cells.Merge(0,0,1,13);
        Cell headerCell = worksheet.Cells["A1"];
        worksheet.Cells["A1"].PutValue("Штапики");
        headerCell.SetStyle(SetFirstHeaderStyle(headerCell));
        _rawOffset++;
        
        worksheet.Cells.Merge(1,0,2,2);
        worksheet.Cells["A2"].PutValue("Маркировка");
        worksheet.Cells["A2"].SetStyle(SetBasicCellStyle(worksheet.Cells["A2"]));
        _rawOffset++;
        
        worksheet.Cells.Merge(1,2,1,8);
        worksheet.Cells["C2"].PutValue("Размеры");
        worksheet.Cells["C2"].SetStyle(SetBasicCellStyle(worksheet.Cells["C2"]));
        
        worksheet.Cells.Merge(1,10,2,1);
        worksheet.Cells["K2"].PutValue("Кол-во,\nшт");
        worksheet.Cells["K2"].SetStyle(SetBasicCellStyle(worksheet.Cells["K2"], fontSize: 10));
        
        worksheet.Cells["C3"].PutValue("L, мм");
        worksheet.Cells["C3"].SetStyle(SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["E3"].PutValue("уг 1-°");
        worksheet.Cells["E3"].SetStyle(SetBasicCellStyle(worksheet.Cells["C3"]));
        
        worksheet.Cells["F3"].PutValue("уг 2-°");
        worksheet.Cells["F3"].SetStyle(SetBasicCellStyle(worksheet.Cells["C3"]));
        _rawOffset++;

        //Writing up details
        foreach (var whipField in whipFields)
        {
            worksheet.Cells.Merge(_rawOffset-1,0,1,13);
            worksheet.Cells["A" + _rawOffset].PutValue($"Штапик {whipField.Whip.Label}, {whipField.Whip.Color} (хлысь {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
            _rawOffset++;
            
            int startRowOffset = _rawOffset;
        
            GetDetails(worksheet , whipField.Details);
            
            worksheet.Cells.Merge(_rawOffset-1,0,1,9);
            worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
            worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K{startRowOffset}:K{_rawOffset - 1})";
            worksheet.Cells["K" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
            _rawOffset++;
            
            
        }
        
        
        
        worksheet.Cells.Merge(_rawOffset-1,0,1,13);
        worksheet.Cells["A" + _rawOffset].PutValue("Штапики. Оптимизация распила");
        worksheet.Cells["A"+_rawOffset].SetStyle(SetFirstHeaderStyle(worksheet.Cells["A" + _rawOffset]));
        _rawOffset++;
        
        //Writing up cutter result
        foreach (var whipField in whipFields)
        {
            worksheet.Cells["K" + _rawOffset].PutValue("ост мм");
            worksheet.Cells["K" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["K"+_rawOffset]));
        
            worksheet.Cells["L" + _rawOffset].PutValue("кол. хл.");
            worksheet.Cells["L" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["L"+_rawOffset]));
        
        
            worksheet.Cells.Merge(_rawOffset-1,0,1,10);
            worksheet.Cells["A" + _rawOffset].PutValue($"Штапик {whipField.Whip.Label}, {whipField.Whip.Color} (хлысь {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["A"+_rawOffset], isBold: true));
            _rawOffset++;


            GetWhipsFromCutter(_rawOffset, worksheet, whipField);
            
        }
        
    }
    
    

    private static Style SetBasicCellStyle(Cell cell, int fontSize = 11, bool isBold = false, bool isColored = false)
    {
        Style basicCellStyle = cell.GetStyle();
        basicCellStyle.HorizontalAlignment = TextAlignmentType.Center;
        basicCellStyle.VerticalAlignment = TextAlignmentType.Center;
        basicCellStyle.Font.Name = "ISOCPEUR";
        basicCellStyle.Font.Size = fontSize;
        basicCellStyle.Font.IsBold = isBold;
        if (isColored)
        {
            basicCellStyle.ForegroundColor = Color.LightYellow;
            basicCellStyle.Pattern = BackgroundType.Solid;
        }

        return basicCellStyle;
    }
    
    
    private static Style SetFirstHeaderStyle(Cell cell)
    {
        Style headerStyleFirst = cell.GetStyle();
        headerStyleFirst.HorizontalAlignment = TextAlignmentType.Center;
        headerStyleFirst.Font.Name = "ISOCPEUR";
        headerStyleFirst.Font.Size = 13;
        headerStyleFirst.Font.IsBold = true;
        return headerStyleFirst;
    }

    private static void GetDetails(Worksheet worksheet, ObservableCollection<Detail> details)
    {
        string detailLenghtColumn = "C";
        string detailAmountColumn = "K";
        string firstDegreesValueColumn = "E";
        string secondDegreesValueColumn = "F";
        

        for (int i = 0; i < details.Count; i++)
        {
            string currentRaw = _rawOffset.ToString(); 
                                 
            worksheet.Cells[detailLenghtColumn + currentRaw].PutValue(details[i].Lenght);
            worksheet.Cells[detailLenghtColumn + currentRaw].SetStyle(
                SetBasicCellStyle(worksheet.Cells[detailLenghtColumn + _rawOffset]));
            
            worksheet.Cells[detailAmountColumn + currentRaw].PutValue(details[i].Amount);
            worksheet.Cells[detailAmountColumn + currentRaw].SetStyle(
                SetBasicCellStyle(worksheet.Cells[detailAmountColumn + _rawOffset]));

            worksheet.Cells[firstDegreesValueColumn + currentRaw].PutValue("90°");
            worksheet.Cells[firstDegreesValueColumn + currentRaw].SetStyle(SetBasicCellStyle(worksheet.Cells[firstDegreesValueColumn + currentRaw]));
            
            worksheet.Cells[secondDegreesValueColumn + currentRaw].PutValue("90°");
            worksheet.Cells[secondDegreesValueColumn + currentRaw].SetStyle(SetBasicCellStyle(worksheet.Cells[secondDegreesValueColumn + currentRaw]));
            _rawOffset ++;
        }
    }

    private static void GetWhipsFromCutter(int rawOffset, Worksheet worksheet, WhipFieldViewModel whipField)
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
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(SetBasicCellStyle(
                        worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
                    columnOffsetIndex++;
                }
                else if (columnOffsetIndex == 10)
                {
                    columnOffsetIndex = 0;
                    rawOffset++;
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(detail);
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(SetBasicCellStyle(
                        worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
                    columnOffsetIndex++;
                    
                }
            }

            columnOffsetIndex = 10;
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(whip.LenghtRemains);
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(SetBasicCellStyle(
                worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));

            columnOffsetIndex = 11;
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(whip.Amount);
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(SetBasicCellStyle(
                worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
            columnOffsetIndex = 0;
            rawOffset++;
        }

        worksheet.Cells.Merge(rawOffset - 1, 0, 1, 10);
        worksheet.Cells["K" + rawOffset].PutValue("Итого:");
        worksheet.Cells["L" + rawOffset].Formula = $"=SUM(L{startRawOffset}:L{rawOffset - 1})";
        worksheet.Cells["L" + rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["L" + rawOffset]));
        rawOffset++;
        _rawOffset = rawOffset;
    }

    private static ObservableCollection<WhipFieldViewModel> GetFrameWhips()
    {
        ObservableCollection<WhipFieldViewModel> result = FrameCutterViewModel.WhipFieldsViewModel;
        return result;
    } 
    
    private static ObservableCollection<WhipFieldViewModel> GetSashWhips()
    {
        ObservableCollection<WhipFieldViewModel> result = SashCutterViewModel.WhipFieldsViewModel;
        return result;
    } 
    
    
}