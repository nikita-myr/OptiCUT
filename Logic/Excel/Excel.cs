using System.Collections.Generic;
using System.Drawing;
using Aspose.Cells;
using OptiCUT.Objects;

namespace OptiCUT.Excel;

/*public static class Excel
{
    private static int _rawOffset = 1;
    public static void GenerateFrameCutExcel(string fileName = "NewWB", string fileType = ".xlsx", bool makePdf = false)
    {
        Workbook workbook = new Workbook();
        
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
        
        //TODO: брать имя стойки из распила.
        worksheet.Cells.Merge(3,0,1,13);
        worksheet.Cells["A" + _rawOffset].PutValue($"Стойка {Program.whip_206.Label}, {Program.whip_206.Color} (хлысь {Program.whip_206.Lenght} мм)");
        worksheet.Cells["A4"].SetStyle(SetBasicCellStyle(worksheet.Cells["A4"], isBold: true, isColored: true));
        _rawOffset++;
        
        GetDetails(worksheet , Program.frameDetails);
        
        worksheet.Cells.Merge(_rawOffset-1,0,1,9);
        worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
        worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K5:K{_rawOffset - 1})";
        worksheet.Cells["K" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
        _rawOffset++;
        

        worksheet.Cells.Merge(_rawOffset-1,0,1,13);
        worksheet.Cells["A" + _rawOffset].PutValue("Каркас. Оптимизация распила");
        worksheet.Cells["A"+_rawOffset].SetStyle(SetFirstHeaderStyle(worksheet.Cells["A" + _rawOffset]));
        _rawOffset++;
        
        worksheet.Cells["K" + _rawOffset].PutValue("ост мм");
        worksheet.Cells["K" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["K"+_rawOffset]));
        
        worksheet.Cells["L" + _rawOffset].PutValue("кол. хл.");
        worksheet.Cells["L" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["L"+_rawOffset]));
        
        
        worksheet.Cells.Merge(_rawOffset-1,0,1,10);
        worksheet.Cells["A" + _rawOffset].PutValue($"Стойка {Program.whip_206.Label}, {Program.whip_206.Color} (хлысь {Program.whip_206.Lenght} мм)");
        worksheet.Cells["A" + _rawOffset].SetStyle(SetBasicCellStyle(worksheet.Cells["A"+_rawOffset], isBold: true));
        _rawOffset++;


        GetWhipsFromCutter(_rawOffset, worksheet, Program.frameDetails);
        
        
        
        

        if (makePdf) workbook.Save(GetSaveDirectory() + fileName + ".pdf");
        workbook.Save(GetSaveDirectory() + fileName + fileType);
        
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

    private static void GetDetails(Worksheet worksheet, List<Detail> details)
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

    private static void GetWhipsFromCutter(int rawOffset, Worksheet worksheet, List<Detail> details)
    {
        List<string> columnIndex = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L"
        };

        int columnOffsetIndex = 0;

        int startRawOffset = rawOffset;

        List<CuttedWhip> cutResult = Cutter.Cut(Program.frameDetails, Program.whip_206);

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
    }
    
    private static string GetSaveDirectory()
    {
        string saveDirectory = "/Users/nikita/Dev/C#_projects/Tester/Tester/ExcelStorage/";
        
        return saveDirectory;
    }
}*/