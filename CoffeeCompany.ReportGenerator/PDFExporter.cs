namespace EntityFrameworkHomework.Client
{
    using System;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.Data.SqlClient;

    public class PDFExporter
    {
        public Document document;
        public string Path { get; set; }
        public string Connection { get; set; }

        public PDFExporter()
        {
            this.document = new Document(PageSize.A4);
            this.Path = "Doc1.pdf";
        }

        public PDFExporter(string path)
            : this()
        {
            this.Path = path;
        }

        public PDFExporter(float width, float height)
            : this()
        {
            this.document = new Document(new Rectangle(width, height));
        }

        public PDFExporter(string connection)
            : this()
        {
            this.Connection = connection;
        }

        public void SaveToDisk()
        {
            PdfWriter.GetInstance(document, new FileStream(Path, FileMode.Create));
        }

        public void WriteLine(string line)
        {
            this.document.Open();
            this.document.Add(new Paragraph(line));
            this.document.Close();
        }

        public void SaveTable(int colls,string query,string title)
        {
            PdfPTable table = new PdfPTable(colls);
            PdfPCell cell = new PdfPCell(new Phrase(title));
            table.AddCell(cell);

            using (SqlConnection conn = new SqlConnection(Connection))
            {
                //string query = "SELECT ProductID, ProductName FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            table.AddCell(rdr[0].ToString());
                            table.AddCell(rdr[1].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                this.document.Open();
                this.document.Add(table);
                this.document.Close();
            }
        }

    }
}
