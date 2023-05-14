using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aspose.Cells;
using OptiCUT.Excel;
using OptiCUT.ViewModels;

namespace OptiCUT.Models.Excel.AvangardStyledExcel;

public class SashCutSheet
{
    private static int _rawOffset;

    public static void GenerateSheet(Workbook workbook, ObservableCollection<Position> whipFields,
                                    string objectLabel = "объект", string constructionLabel = "В-X")
    {
        _rawOffset = 1;

        Worksheet worksheet = workbook.Worksheets[workbook.Worksheets.Add()];
        worksheet.Name = "Штапики";

        worksheet.Cells.Merge(0, 0, 1, 13);
        Cell headerCell = worksheet.Cells["A1"];
        worksheet.Cells["A1"].PutValue("Штапики");
        headerCell.SetStyle(CellStyles.SetFirstHeaderStyle(headerCell));
        _rawOffset++;

        worksheet.Cells.Merge(1, 0, 2, 2);
        worksheet.Cells["A2"].PutValue("Маркировка");
        worksheet.Cells["A2"].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A2"]));
        _rawOffset++;

        worksheet.Cells.Merge(1, 2, 1, 8);
        worksheet.Cells["C2"].PutValue("Размеры");
        worksheet.Cells["C2"].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C2"]));

        worksheet.Cells.Merge(1, 10, 2, 1);
        worksheet.Cells["K2"].PutValue("Кол-во,\nшт");
        worksheet.Cells["K2"].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["K2"], fontSize: 10));

        worksheet.Cells["C3"].PutValue("L, мм");
        worksheet.Cells["C3"].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["E3"].PutValue("уг 1-°");
        worksheet.Cells["E3"].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C3"]));

        worksheet.Cells["F3"].PutValue("уг 2-°");
        worksheet.Cells["F3"].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C3"]));
        _rawOffset++;

        //Writing up details
        foreach (var whipField in whipFields)
        {
            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
            worksheet.Cells["A" + _rawOffset]
                .PutValue($"{whipField.Whip.Label}, {whipField.Whip.Color} (хлыст {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset]
                .SetStyle(
                    CellStyles.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true, isColored: true));
            _rawOffset++;

            int startRowOffset = _rawOffset;

            GetDetails(worksheet, whipField.Details);

            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 9);
            worksheet.Cells["J" + _rawOffset].PutValue("Итого:");
            worksheet.Cells["K" + _rawOffset].Formula = $"=SUM(K{startRowOffset}:K{_rawOffset - 1})";
            worksheet.Cells["K" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
            _rawOffset++;
        }



        worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 13);
        worksheet.Cells["A" + _rawOffset].PutValue("Штапики. Оптимизация распила");
        worksheet.Cells["A" + _rawOffset].SetStyle(CellStyles.SetFirstHeaderStyle(worksheet.Cells["A" + _rawOffset]));
        _rawOffset++;

        //Writing up cutter result
        foreach (var whipField in whipFields)
        {
            worksheet.Cells["K" + _rawOffset].PutValue("ост мм");
            worksheet.Cells["K" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));

            worksheet.Cells["L" + _rawOffset].PutValue("кол. хл.");
            worksheet.Cells["L" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["L" + _rawOffset]));


            worksheet.Cells.Merge(_rawOffset - 1, 0, 1, 10);
            worksheet.Cells["A" + _rawOffset]
                .PutValue($"{whipField.Whip.Label}, {whipField.Whip.Color} (хлыст {whipField.Whip.Lenght} мм)");
            worksheet.Cells["A" + _rawOffset]
                .SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isBold: true));
            _rawOffset++;


            GetWhipsFromCutter(_rawOffset, worksheet, whipField);

        }

        CellStyles.DrawBorders(workbook, worksheet, _rawOffset);

        worksheet.PageSetup.FitToPagesWide = 1;
        worksheet.PageSetup.PrintArea = $"A1:M{_rawOffset}";
        worksheet.PageSetup.SetHeader(1, $"{objectLabel} {constructionLabel}");
    }

    private static void GetDetails(Worksheet worksheet, ObservableCollection<Detail> details)
    {
        for (int i = 0; i < details.Count; i++)
        {
            worksheet.Cells["C" + _rawOffset].PutValue(details[i].Lenght);
            worksheet.Cells["C" + _rawOffset].SetStyle(
                CellStyles.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));

            worksheet.Cells["K" + _rawOffset].PutValue(details[i].Amount);
            worksheet.Cells["K" + _rawOffset].SetStyle(
                CellStyles.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));

            worksheet.Cells["E" + _rawOffset].PutValue("90°");
            worksheet.Cells["E" + _rawOffset]
                .SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));

            worksheet.Cells["F" + _rawOffset].PutValue("90°");
            worksheet.Cells["F" + _rawOffset]
                .SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset]));
            _rawOffset++;
        }
    }

    private static void GetWhipsFromCutter(int rawOffset, Worksheet worksheet, Position position)
    {
        List<string> columnIndex = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L"
        };

        int columnOffsetIndex = 0;

        int startRawOffset = rawOffset;

        List<CuttedWhip> cutResult = Cutter.Cut(position.Details, position.Whip);

        foreach (CuttedWhip whip in cutResult)
        {
            foreach (float detail in whip.Details)
            {
                if (columnOffsetIndex < 10)
                {
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(detail);
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellStyles.SetBasicCellStyle(
                        worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
                    columnOffsetIndex++;
                }
                else if (columnOffsetIndex == 10)
                {
                    columnOffsetIndex = 0;
                    rawOffset++;
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(detail);
                    worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellStyles.SetBasicCellStyle(
                        worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
                    columnOffsetIndex++;

                }
            }

            columnOffsetIndex = 10;
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(whip.LenghtRemains);
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellStyles.SetBasicCellStyle(
                worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));

            columnOffsetIndex = 11;
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].PutValue(whip.Amount);
            worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset].SetStyle(CellStyles.SetBasicCellStyle(
                worksheet.Cells[columnIndex[columnOffsetIndex] + rawOffset]));
            columnOffsetIndex = 0;
            rawOffset++;
        }

        worksheet.Cells.Merge(rawOffset - 1, 0, 1, 10);
        worksheet.Cells["K" + rawOffset].PutValue("Итого:");
        worksheet.Cells["L" + rawOffset].Formula = $"=SUM(L{startRawOffset}:L{rawOffset - 1})";
        worksheet.Cells["L" + rawOffset].SetStyle(style: CellStyles.SetBasicCellStyle(worksheet.Cells["L" + rawOffset]));
        rawOffset++;
        _rawOffset = rawOffset;
    }
}