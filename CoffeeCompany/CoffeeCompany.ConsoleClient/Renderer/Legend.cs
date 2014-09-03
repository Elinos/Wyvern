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
            "\n To export reports type *Export*" +
            "\n" +
            "\n To load files to the database type *Load*";

        public const string ExportLegend =
            "Welcome to the export interface" +
            "\n" +
            "\n-----to export as Json Object type *Json*" +
            "\n" +
            "\n-----to export as Xml docummnet type *Xml*" +
            "\n" +
            "\n-----to export a Pdf report type *Pdf*" +
            "\n" +
            "\n-----to export Excel report type *Excel*"+
             "\n" +
            "\n-----to go back type *Back*";

        public const string LoadLegend =
            "Welcom to the Loading interface" +
            "\n" +
            "\n------to load from Excel file type *Excel*" +
            "\n" +
            "\n------to load from MongoDB database type *Mongo*" +
            "\n" +
            "\n------to load from XML file type *Xml*" +
             "\n" +
            "\n-----to go back type *Back*";

        public const string ExecutingCommand = "Executing Command";
    }
}
