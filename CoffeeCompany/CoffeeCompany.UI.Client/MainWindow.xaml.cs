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
        private ReportsEngine reportGnerator = new ReportsEngine();

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
            bool exportPendingOrders = PDFPendingCheckBox.IsChecked.Value;
            bool exportCompanyOrders = PDFCompanyCheckBox.IsChecked.Value;
            var companyName = PDFCompanyNameTB.Text;
            if (exportPendingOrders)
            {
                reportGnerator.GetPendingOrdersPdfReport(@"..\..\Reports\pendingOrdersReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            }
            if (exportCompanyOrders && companyName != "" & companyName != "Company Name")
            {
                reportGnerator.GetOrderForCompanyPdfReport(companyName, @"..\..\Reports\companyOrdersReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            }
            else
            {
                Result.Text = "Enter Company Name!";
                Result.Foreground = Brushes.Red;
            }
        }

        private void ToJSONButton_Click(object sender, RoutedEventArgs e)
        {
            reportGnerator.GetJsonOrderInfoReport();
        }

        private void ToXMLButton_Click(object sender, RoutedEventArgs e)
        {
            bool exportPendingOrders = PDFPendingCheckBox.IsChecked.Value;
            bool exportCompanyOrders = PDFCompanyCheckBox.IsChecked.Value;
            var companyName = XMLCompanyNameTB.Text;
            if (exportPendingOrders)
            {
                reportGnerator.GetPendingOrdersXmlReport(@"..\..\Reports\pendingOrdersReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");

            }
            if (exportCompanyOrders && companyName != "" & companyName != "Company Name")
            {
                reportGnerator.GetOrderForCompanyXmlReport(companyName, @"..\..\Reports\companyOrdersReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");
            }
            else
            {
                Result.Text = "Enter Company Name!";
                Result.Foreground = Brushes.Red;
            }
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
            sqLiteManager.DeleteAllEntities("Discounts");
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

        private void PDFCompanyNameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            PDFCompanyNameTB.Text = "";
        }
        private void XMLCompanyNameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            XMLCompanyNameTB.Text = "";
        }
    }
}
