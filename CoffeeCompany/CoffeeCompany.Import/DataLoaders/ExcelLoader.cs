namespace CoffeeCompany.Import.DataLoaders
{
    using System.Collections.Generic;

    using Ionic.Zip;

    using CoffeeCompany.Excel.Manager;
    using CoffeeCompany.Models;

    internal class ExcelLoader
    {
        private const string ClientCompanyFileName = "ClientCompany.xlsx";
        private const string ProductFileName = "Product.xlsx";
        private string zipToUnpack;
        private string unpackDirectory;

        public ExcelLoader(string zipToUnpack, string unpackDirectory)
        {
            this.zipToUnpack = zipToUnpack;
            this.unpackDirectory = unpackDirectory;

            this.ExtractFilesFromZip(zipToUnpack, unpackDirectory);
        }

        private void ExtractFilesFromZip(string zipToUnpack, string unpackDirectory)
        {
            using (ZipFile zipEntries = ZipFile.Read(zipToUnpack))
            {
                foreach (ZipEntry zipEntry in zipEntries)
                {
                    zipEntry.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        public ICollection<ClientCompany> retrieveCompaniesData()
        {
            var excelManager = new ExcelManager();
            var filePath = this.unpackDirectory + "/" + ClientCompanyFileName;

            return excelManager.ReadClientCompanyExcelFile(filePath);
        }

        public ICollection<Product> retrieveProductData()
        {
            var excelManager = new ExcelManager();
            var filePath = this.unpackDirectory + "/" + ProductFileName;

            return excelManager.ReadProductExcelFile(filePath);
        }
    }
}
