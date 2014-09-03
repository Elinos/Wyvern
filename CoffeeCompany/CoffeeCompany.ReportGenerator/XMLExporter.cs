namespace CoffeeCompany.ReportGenerator
{
    using System.Collections.Generic;
    using System.Xml;

    public class XMLExporter
    {
        public XmlDocument document;

        public XMLExporter()
        {
            this.document = new XmlDocument();
        }

        public void ExportDocument(List<List<string>> content, string title, List<string> elements, string path)
        {
            XmlElement outerElement = document.CreateElement("Report");

            outerElement.SetAttribute("title", null, title);

            foreach (var set in content)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    XmlElement element = document.CreateElement(elements[i]);
                    element.InnerText = set[i];
                    outerElement.AppendChild(element);

                }
            }

            this.document.Save(path);
        }

    }
}

