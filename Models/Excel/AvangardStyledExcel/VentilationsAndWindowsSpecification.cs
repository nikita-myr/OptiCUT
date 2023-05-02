using System.Collections.ObjectModel;
using Aspose.Cells;

namespace OptiCUT.Models.Excel.AvangardStyledExcel;

public class VentilationsAndWindowsSpecification
{
    private static int _rawOffset;
    
    public static void GenerateSpecification(Workbook workbook, ObservableCollection<Window> windows, 
        ObservableCollection<Ventilation> ventilations, string objectLabel, string constructionLabel)
    {
        _rawOffset = 1;
        
        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = "Спец. створок и ВР";
        
        worksheet.Cells.SetColumnWidth(0,14);
        worksheet.Cells.SetColumnWidth(1,11);
        worksheet.Cells.SetColumnWidth(2,11);
        worksheet.Cells.SetColumnWidth(3,10);
        worksheet.Cells.SetColumnWidth(4,10);
        worksheet.Cells.SetColumnWidth(5,10);
        worksheet.Cells.SetColumnWidth(6,10);
        worksheet.Cells.SetColumnWidth(9,10);
        worksheet.Cells.SetColumnWidth(10,10);
        worksheet.Cells.SetColumnWidth(11,10);
        worksheet.Cells.SetRowHeight(2, 40);
        
        worksheet.Cells.Merge(0, 0, 1, 12);
        worksheet.Cells["A"+ _rawOffset].PutValue($"{objectLabel} {constructionLabel}. Спецификация ВР.");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A"+_rawOffset]));
        _rawOffset++;
        
        worksheet.Cells.Merge(_rawOffset-1, 0, 2, 1);
        worksheet.Cells["A"+ _rawOffset].PutValue($"Маркировка ВР");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset]));

        worksheet.Cells.Merge(_rawOffset-1, 1, 1, 2);
        worksheet.Cells["B" + _rawOffset].PutValue("Габариты ВР");
        worksheet.Cells["B" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["B" + _rawOffset]));
        
        worksheet.Cells.Merge(_rawOffset-1, 3, 2, 1);
        worksheet.Cells["D" + _rawOffset].PutValue("Кол-во, шт");
        worksheet.Cells["D" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["D" + _rawOffset]));

        worksheet.Cells.Merge(_rawOffset-1, 4, 1, 2);
        worksheet.Cells["E" + _rawOffset].PutValue("Размер рамы, мм");
        worksheet.Cells["E" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));
        
        worksheet.Cells.Merge(_rawOffset-1, 6, 2, 2);
        worksheet.Cells["G" + _rawOffset].PutValue("Примичание (цвет)");
        worksheet.Cells["G" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["G" + _rawOffset]));
        
        worksheet.Cells.Merge(_rawOffset-1, 9, 1, 3);
        worksheet.Cells["J" + _rawOffset].PutValue("Ламели");
        worksheet.Cells["J" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["J" + _rawOffset]));
        _rawOffset++;
        
        worksheet.Cells["B" + _rawOffset].PutValue("Высота, мм.");
        worksheet.Cells["B" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["B" + _rawOffset]));

        worksheet.Cells["C" + _rawOffset].PutValue("Ширина, мм.");
        worksheet.Cells["C" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));

        worksheet.Cells["E" + _rawOffset].PutValue("лево/право");
        worksheet.Cells["E" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));
        
        worksheet.Cells["F" + _rawOffset].PutValue("верх/низ");
        worksheet.Cells["F" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset]));
        
        worksheet.Cells["J" + _rawOffset].PutValue("Длинна, мм.");
        worksheet.Cells["J" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["J" + _rawOffset]));
        
        worksheet.Cells["K" + _rawOffset].PutValue("Кол-во на 1 ВР, шт.");
        worksheet.Cells["K" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset], isWrap: true));
        
        worksheet.Cells["L" + _rawOffset].PutValue("Примечание");
        worksheet.Cells["L" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["L" + _rawOffset]));
        _rawOffset++;


        if (ventilations.Count > 0)
        {
            int startRawOffset = _rawOffset;
                
            foreach (Ventilation ventilation in ventilations)
            {
                worksheet.Cells["A" + _rawOffset].PutValue(ventilation.Label);
                worksheet.Cells["A" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset]));
                
                worksheet.Cells["B" + _rawOffset].PutValue(ventilation.Height-43);
                worksheet.Cells["B" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["B" + _rawOffset]));
                
                worksheet.Cells["C" + _rawOffset].PutValue(ventilation.Width-43);
                worksheet.Cells["C" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));
                
                worksheet.Cells["D" + _rawOffset].PutValue(ventilation.Amount);
                worksheet.Cells["D" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["D" + _rawOffset]));
                
                worksheet.Cells["E" + _rawOffset].PutValue(ventilation.Height - 77);
                worksheet.Cells["E" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));
                
                worksheet.Cells["F" + _rawOffset].PutValue(ventilation.Width-43);
                worksheet.Cells["F" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset]));
                
                worksheet.Cells["G" + _rawOffset].PutValue(ventilation.Color);
                worksheet.Cells["G" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["G" + _rawOffset]));
                
                worksheet.Cells["J" + _rawOffset].PutValue(ventilation.Width - 77);
                worksheet.Cells["J" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["J" + _rawOffset]));
                
                worksheet.Cells["K" + _rawOffset].PutValue(3);
                worksheet.Cells["K" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
                
                worksheet.Cells["L" + _rawOffset].PutValue(ventilation.Color);
                worksheet.Cells["L" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["L" + _rawOffset]));
                _rawOffset++;
            }
            
            worksheet.Cells["C" + _rawOffset].PutValue("Итого:");
            worksheet.Cells["C" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));
            
            worksheet.Cells["D" + _rawOffset].Formula = $"=SUM(D{startRawOffset}:D{_rawOffset-1})";
            worksheet.Cells["D" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["D" + _rawOffset]));
            
            
            _rawOffset++;
            
        }
        
        worksheet.Cells.Merge(_rawOffset-1, 0, 1, 12);
        worksheet.Cells["A"+ _rawOffset].PutValue($"{objectLabel} {constructionLabel}. Спецификация створок.");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A"+_rawOffset]));
        _rawOffset++;
        
        worksheet.Cells.Merge(_rawOffset-1, 0, 2, 1);
        worksheet.Cells["A"+ _rawOffset].PutValue($"Маркировка створок");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset], isColored:true, isWrap:true));
        worksheet.Cells.SetRowHeight(_rawOffset-1, 40);

        worksheet.Cells.Merge(_rawOffset-1, 1, 1, 2);
        worksheet.Cells["B" + _rawOffset].PutValue("Размеры");
        worksheet.Cells["B" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["B" + _rawOffset], isColored:true));
        
        worksheet.Cells.Merge(_rawOffset-1, 3, 2, 1);
        worksheet.Cells["D" + _rawOffset].PutValue("Кол-во, шт");
        worksheet.Cells["D" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["D" + _rawOffset], isColored:true));
        
        worksheet.Cells.Merge(_rawOffset-1, 4, 1, 2);
        worksheet.Cells["E" + _rawOffset].PutValue("ОТКРЫВАНИЯ");
        worksheet.Cells["E" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset], isColored:true));
        
        worksheet.Cells.Merge(_rawOffset-1, 6, 2, 2);
        worksheet.Cells["G" + _rawOffset].PutValue("Примичание");
        worksheet.Cells["G" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["G" + _rawOffset], isColored: true));
        
        worksheet.Cells.Merge(_rawOffset-1, 8, 1, 4);
        worksheet.Cells["I" + _rawOffset].PutValue("СТЕКЛО");
        worksheet.Cells["I" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["I" + _rawOffset], isColored:true));
        _rawOffset++;
        
        worksheet.Cells["B" + _rawOffset].PutValue("Высота, мм.");
        worksheet.Cells["B" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["B" + _rawOffset], isColored:true, isWrap:true));
        
        worksheet.Cells["C" + _rawOffset].PutValue("Ширина, мм.");
        worksheet.Cells["C" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset], isColored:true, isWrap:true));
        
        worksheet.Cells["E" + _rawOffset].PutValue("лев/прав");
        worksheet.Cells["E" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset], isColored:true, fontSize:10));
        
        worksheet.Cells["F" + _rawOffset].PutValue("пов/пов-отк");
        worksheet.Cells["F" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset], isColored:true, fontSize:10));
        
        worksheet.Cells["I" + _rawOffset].PutValue("тол. Стекла, мм.");
        worksheet.Cells["I" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["I" + _rawOffset], isColored:true, isWrap:true));
        
        worksheet.Cells["J" + _rawOffset].PutValue("Марк.");
        worksheet.Cells["J" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["J" + _rawOffset], isColored:true, fontSize:10));
        
        worksheet.Cells["K" + _rawOffset].PutValue("Высота, мм.");
        worksheet.Cells["K" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset], isColored:true, fontSize:10));
        
        worksheet.Cells["L" + _rawOffset].PutValue("Ширина, мм.");
        worksheet.Cells["L" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["L" + _rawOffset], isColored:true, fontSize:10));
        _rawOffset++;

        if (windows.Count > 0)
        {
            int startRawOffset = _rawOffset;
            
            foreach (Window window in windows)
            {
                worksheet.Cells["A" + _rawOffset].PutValue($"{window.Label}({constructionLabel})");
                worksheet.Cells["A" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A" + _rawOffset]));
                
                worksheet.Cells["B" + _rawOffset].PutValue(window.Height);
                worksheet.Cells["B" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["B" + _rawOffset]));
                
                worksheet.Cells["C" + _rawOffset].PutValue(window.Width);
                worksheet.Cells["C" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));
                
                worksheet.Cells["D" + _rawOffset].PutValue(window.Amount);
                worksheet.Cells["D" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["D" + _rawOffset]));
                
                worksheet.Cells["E" + _rawOffset].PutValue(window.OpenSide);
                worksheet.Cells["E" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["E" + _rawOffset]));
                
                worksheet.Cells["F" + _rawOffset].PutValue(window.OpenType);
                worksheet.Cells["F" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["F" + _rawOffset]));
                
                worksheet.Cells["G" + _rawOffset].PutValue(window.Color);
                worksheet.Cells["G" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["G" + _rawOffset]));

                if (window.IsNeedBrush)
                {
                    worksheet.Cells["H" + _rawOffset].PutValue("гребенка");
                    worksheet.Cells["H" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["H" + _rawOffset]));
                }
                
                /*worksheet.Cells["I" + _rawOffset].PutValue(window.Glass.Depth);
                worksheet.Cells["I" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["I" + _rawOffset]));
                
                worksheet.Cells["K" + _rawOffset].PutValue(window.Glass.Height);
                worksheet.Cells["K" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["K" + _rawOffset]));
                
                worksheet.Cells["L" + _rawOffset].PutValue(window.Glass.Width);
                worksheet.Cells["L" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["L" + _rawOffset]));
                */
                _rawOffset++;
            }
            
            worksheet.Cells["C" + _rawOffset].PutValue("Итого:");
            worksheet.Cells["C" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["C" + _rawOffset]));
            
            worksheet.Cells["D" + _rawOffset].Formula = $"=SUM(D{startRawOffset}:D{_rawOffset-1})";
            worksheet.Cells["D" + _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["D" + _rawOffset]));
            _rawOffset++;
        }
        
        worksheet.Cells.Merge(_rawOffset-1, 0, 1, 12);
        worksheet.Cells["A"+ _rawOffset].PutValue("ЛЕВОЕ- > открывается внутрь помещения справа-налево (левой рукой на себя)");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A"+_rawOffset]));
        _rawOffset++;
        
        worksheet.Cells.Merge(_rawOffset-1, 0, 1, 12);
        worksheet.Cells["A"+ _rawOffset].PutValue("ПРАВОЕ- < открываетсявнутрь помещения  слева-направо (правой рукой на себя)");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A"+_rawOffset]));
        _rawOffset++;
        
        worksheet.Cells.Merge(_rawOffset-1, 0, 1, 12);
        worksheet.Cells["A"+ _rawOffset].PutValue("Высота установки ручки - 500 мм от низа створки");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A"+_rawOffset]));
        _rawOffset++;
        
        worksheet.Cells.Merge(_rawOffset-1, 0, 1, 12);
        worksheet.Cells["A"+ _rawOffset].PutValue("Для створок выше 1500мм устанавливаем доп. ответную часть(1597K), доп. цапфу эксцентриковую(1596K) и прижим скрытый (1622)");
        worksheet.Cells["A"+ _rawOffset].SetStyle(CellStyles.SetBasicCellStyle(worksheet.Cells["A"+_rawOffset], fontSize:8));

        CellStyles.DrawBorders(workbook, worksheet, _rawOffset);

        worksheet.PageSetup.FitToPagesWide = 1;
        worksheet.PageSetup.PrintArea = $"A1:M{_rawOffset}";
        
    }
    
}