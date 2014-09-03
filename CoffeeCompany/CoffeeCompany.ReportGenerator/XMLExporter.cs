namespace CoffeeCompany.ReportGenerator
{
    using System.Collections.Generic;
    using System.Xml;

    public class XMLExporter
    {
        public XmlDocument document;

        private string StringConcatenatior(string rawString)
        {
            var parts = rawString.Split(' ');
            var parsedString = string.Join("", parts);

            return parsedString;
        }

        public void ExportDocument(List<List<string>> content, string title, List<string> elements, string path)
        {
            this.document = new XmlDocument();
            document.LoadXml("<?xml version='1.0' ?>" +
    "<ProductsReports>" +
    "</ProductsReports>");

            XmlNode root = document.DocumentElement;
            XmlElement outerElement = document.CreateElement("Report");

            var parsedTitle = StringConcatenatior(title);
            outerElement.SetAttribute("title", parsedTitle);
            outerElement.Prefix = "";
            root.AppendChild(outerElement);

            foreach (var set in content)
            {
                XmlElement productInfo = document.CreateElement("ProductInfo");
                for (int i = 0; i < elements.Count; i++)
                {
                    var parsedElement = StringConcatenatior(elements[i]);
                    XmlElement element = document.CreateElement(parsedElement);
                    element.InnerText = set[i];
                    productInfo.AppendChild(element);
                }

                outerElement.AppendChild(productInfo);
            }

            this.document.Save(path);
        }

    }
}

