using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompany.ConsoleClient.Renderer
{
    public class ConsoleRenderer
    {

        public ConsoleRenderer() 
        {
            this.Legend = new Legend();
        }

        internal Legend Legend { get; set; }

        public void PrintLegend()
        {
            Console.Clear();
            Console.WriteLine(Legend.comandLegend);
        }

        public void PrintExportLegend()
        {
            Console.Clear();
            Console.WriteLine(Legend.ExportLegend);
        }

        public void PrintLoadLegend()
        {
            Console.Clear();
            Console.WriteLine(Legend.LoadLegend);
        }

        public void PrintCustomReportLegend()
        {
            Console.Clear();
            Console.WriteLine(Legend.CustomReportLegend);
        }

        public void InvalidCommandMessage()
        {
            Console.Clear();
            Console.WriteLine("Invalid Command");
        }

        public void CompletedMessage()
        {
            Console.Clear();
            Console.WriteLine(Legend.Completed);
        }


    }
}
