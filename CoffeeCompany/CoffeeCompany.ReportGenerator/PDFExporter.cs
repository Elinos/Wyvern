namespace CoffeeCompany.ReportGenerator
{
    using System;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using System.Collections;

    public class PDFExporter
    {
        public Document document;
        public string Path { get; set; }

        public PDFExporter()
        {
            this.document = new Document(PageSize.A4);
        }

        private void SaveToDisk(string path)
        {
            this.Path = path;
            PdfWriter.GetInstance(document, new FileStream(Path, FileMode.Create));
        }

        public void GetPDF(List<List<string>> items, string title, IList<string> cells, string path)
        {
            this.SaveToDisk(path);

            PdfPTable table = new PdfPTable(cells.Count);
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false);
            Font headerFont = new Font(bfTimes, 16, Font.BOLD);
            PdfPCell cell = new PdfPCell(new Phrase(title, headerFont));
            cell.Colspan = cells.Count;
            cell.HorizontalAlignment = 1;
            cell.Border = 0;
            table.AddCell(cell);

            PdfPCell empty = new PdfPCell(new Phrase(" "));
            empty.Border = 0;
            empty.Colspan = cells.Count;
            table.AddCell(empty);

            Font bold = new Font(bfTimes, 12, Font.BOLD);
            foreach (var cellTitle in cells)
            {
                table.AddCell(new PdfPCell(new Phrase(cellTitle, bold)));
            }

            foreach (List<string> rowCells in items)
            {
                for (int i = 0; i < cells.Count; i++)
                {
                    table.AddCell(rowCells[i]);                    
                }
            }

            this.document.Open();
            this.document.Add(table);
            this.document.Close();
        }
    }
}
