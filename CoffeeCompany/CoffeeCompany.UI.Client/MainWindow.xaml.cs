using System;
using System.Linq;
using System.Windows;
using CoffeeCompany.Excel.Manager;
using CoffeeCompany.Import;
using CoffeeCompany.MySQL.Manager;

namespace CoffeeCompany.UI.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private DataImport dataImport = new DataImport();


        private void FromZipFileButton_Click(object sender, RoutedEventArgs e)
        {
            dataImport.ImportFromExcel();
        }

        private void ToExcelFileButton_Click(object sender, RoutedEventArgs e)
        {
            var excelManager = new ExcelManager();
            excelManager.CreateExcelReport();
        }

        private void FromMongoDBButton_Click(object sender, RoutedEventArgs e)
        {
            dataImport.ImportFromMongoDb();
        }

        private void ToPDFButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToJSONButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToXMLButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FromXMLButton_Click(object sender, RoutedEventArgs e)
        {
            dataImport.ImportFromXml();
        }

        private void ToMySQLButton_Click(object sender, RoutedEventArgs e)
        {
            var mySqlManager = new MySQLManager();
            mySqlManager.LoadAllReportsDataFromSQLServer();
        }
    }
}
