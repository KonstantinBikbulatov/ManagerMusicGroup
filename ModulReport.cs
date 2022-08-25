using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MSWord = Microsoft.Office.Interop.Word;

namespace ManagerMusicGroup
{
    public static class ModulReport
    {
        public static void WriteReport(string header, string[] headerColumns, string[,] listItems, string nameFile)
        {
            var wordApp = new MSWord.Application();
            // MessageBox.Show("Word запущен!");
            // 2) покажем окно Word
            //wordApp.Visible = true;

            // 3) добавим докумет!
            wordApp.Documents.Add();
            // wordApp.Documents.Add();
            // wordApp.Documents.Add();

            // 4) добавим текст
            // нужно выбрать документ
            MSWord.Document doc = wordApp.Documents[1];

            doc.Paragraphs.Add();
            MSWord.Paragraph paragraph = doc.Paragraphs[1];

            doc.Paragraphs.Add();
            paragraph = doc.Paragraphs[2];
            paragraph.Range.Text = header;
            // форматирование
            paragraph.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph.Range.Font.Bold = 1;
            paragraph.Range.Font.Color = MSWord.WdColor.wdColorRed;
            
            
            doc.Paragraphs.Add();
            paragraph = doc.Paragraphs[3];
            //paragraph.Range.Text = "Таблица №1";
            //paragraph.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphJustify;
            paragraph.Range.Font.Bold = 0;
            paragraph.Range.Font.Color = MSWord.WdColor.wdColorBlack;
            
            //doc.Paragraphs.Add();
            //paragraph = doc.Paragraphs[3];
            
            doc.Tables.Add(paragraph.Range, listItems.GetLength(1)+1, listItems.GetLength(0), true);
            MSWord.Table table = doc.Tables[1];

            table.Borders.OutsideColor = MSWord.WdColor.wdColorRed;
            table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleDouble;
            table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleDot;

            // данные в таблицу
            for (int i = 1; i < headerColumns.Length + 1; i++)
            {
                table.Cell(1, i).Range.Text = headerColumns[i-1];
            }

            for (int i = 0; i < listItems.GetLength(1); i++)
            {
                for (int j = 0; j < listItems.GetLength(0); j++)
                {
                    table.Cell(i+2, j+1).Range.Text = listItems[j, i];
                }
                
            }

            //wordApp.Visible = true;
            // автоматически сохраним документ
            doc.SaveAs2("w:\\" + nameFile + ".doc", MSWord.WdSaveFormat.wdFormatDocument);
            doc.Close();
            wordApp.Quit();
        }
    }
}
