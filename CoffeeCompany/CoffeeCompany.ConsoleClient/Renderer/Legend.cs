namespace CoffeeCompany.ConsoleClient.Renderer
{
    using System;

    class Legend
    {
        public const string comandLegend =
            "-----------------Command Legend------------------" +
            "\n" +
            "\n" +
            "\n" +
            "\nTo export reports type *Export*" +
            "\n" +
            "\nTo load files to the database type *Load*" +
            "\n";

        public const string ExportLegend =
            "Welcome to the export interface" +
            "\n" +
            "\nto export as Json Object type *Json*" +
            "\n" +
            "\nto export as Xml docummnet type *Xml*" +
            "\n" +
            "\nto export a Pdf report type *Pdf*" +
            "\n" +
            "\nto export Excel report type *Excel*"+
             "\n" +
            "\nto go back type *Back*" +
            "\n";

        public const string LoadLegend =
            "Welcom to the Loading interface" +
            "\n" +
            "\nto load from Excel file type *Excel*" +
            "\n" +
            "\nto load from MongoDB database type *Mongo*" +
            "\n" +
            "\nto load from XML file type *Xml*" +
             "\n" +
            "\nto go back type *Back*" +
            "\n";

        public const string CustomReportLegend = 
            "Choose Report type" +
            "\n" +
            "\nto export pending orders report type  *Pending*" +
            "\n" +
            "\nto export company order report type *Order*" +
             "\n" +
            "\nto go back type *Back*"+
            "\n" ;
    }
}
