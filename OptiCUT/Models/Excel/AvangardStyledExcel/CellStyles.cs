using Aspose.Cells;
using Color = System.Drawing.Color;

namespace OptiCUT.Models.Excel.AvangardStyledExcel;

public class CellStyles
{
    public static Style SetBasicCellStyle(Cell cell, int fontSize = 11, bool isBold = false, bool isColored = false, bool isWrap = false)
    {
        Style basicCellStyle = cell.GetStyle();
        basicCellStyle.HorizontalAlignment = TextAlignmentType.Center;
        basicCellStyle.VerticalAlignment = TextAlignmentType.Center;
        basicCellStyle.Font.Name = "ISOCPEUR";
        basicCellStyle.Font.Size = fontSize;
        basicCellStyle.Font.IsBold = isBold;
        basicCellStyle.IsTextWrapped = isWrap;
        if (isColored)
        {
            basicCellStyle.ForegroundColor = Color.LightYellow;
            basicCellStyle.Pattern = BackgroundType.Solid;
        }

        return basicCellStyle;
    }

    public static Style SetFirstHeaderStyle(Cell cell)
    {
        Style headerStyleFirst = cell.GetStyle();
        headerStyleFirst.HorizontalAlignment = TextAlignmentType.Center;
        headerStyleFirst.Font.Name = "ISOCPEUR";
        headerStyleFirst.Font.Size = 13;
        headerStyleFirst.Font.IsBold = true;
        return headerStyleFirst;
    }
    
    public static void DrawBorders(Workbook workbook, Worksheet worksheet, int rawOffset)
    {
        CellsColor cellsColor = workbook.CreateCellsColor();
        
        Range range = worksheet.Cells.CreateRange(0, 0, rawOffset-1, 13);
        
        range.SetInsideBorders(BorderType.Vertical, CellBorderType.Thin, cellsColor);
        range.SetInsideBorders(BorderType.Horizontal, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.TopBorder, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.LeftBorder, CellBorderType.Thin, cellsColor);
        range.SetOutlineBorder(BorderType.RightBorder, CellBorderType.Thin, cellsColor);
        
    } 
    
}