using Aspose.Cells;
using Avalonia.Media;
using Color = System.Drawing.Color;

namespace OptiCUT.Excel;

public class CellSetups
{
    public static Style SetBasicCellStyle(Cell cell, int fontSize = 11, bool isBold = false, bool isColored = false)
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

    public static Style SetFirstHeaderStyle(Cell cell)
    {
        Style headerStyleFirst = cell.GetStyle();
        headerStyleFirst.HorizontalAlignment = TextAlignmentType.Center;
        headerStyleFirst.Font.Name = "ISOCPEUR";
        headerStyleFirst.Font.Size = 13;
        headerStyleFirst.Font.IsBold = true;
        return headerStyleFirst;
    }
    
}