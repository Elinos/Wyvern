using System;
using System.Linq;
using System.Windows;
using CoffeeCompany.Excel.Manager;
using CoffeeCompany.Import;
using CoffeeCompany.MySQL.Manager;
using CoffeeCompany.ReportGenerator;
using CoffeeCompany.SQLite.Manager;
using System.Windows.Media;

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
            bool result = excelManager.CreateExcelReport();
            if (result)
            {
                Result.Text = "Generating Excel report has been successfully completed!";
                Result.Foreground = Brushes.Green;
            }
            else
            {
                Result.Text = "Generating Excel report failed!";
                Result.Foreground = Brushes.Red;
            }
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
            bool result;
            var mySqlManager = new MySQLManager();
            result = mySqlManager.LoadAllReportsDataFromSQLServer();
            var sqLiteManager = new SQLiteManager();
            var reportsEngine = new ReportsEngine();
            var discounts = reportsEngine.GetDiscountsInfo();
            foreach (var discount in discounts)
            {
               result = sqLiteManager.CreateDiscountForCompany(discount.CompanyId, discount.TypeID);
            }

            if (result)
            {
                Result.Text = "Import to MySQL has been successfully completed!";
                Result.Foreground = Brushes.Green;
            }
            else
            {
                Result.Text = "Import to MySQL failed!";
                Result.Foreground = Brushes.Red;
            }
        }
    }
}
