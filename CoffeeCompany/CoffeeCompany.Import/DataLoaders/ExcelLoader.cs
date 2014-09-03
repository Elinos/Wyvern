namespace CoffeeCompany.Import.DataLoaders
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    using Ionic.Zip;

    using CoffeeCompany.Excel.Manager;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import.DataLoaders.Contracts;

    internal class ExcelLoader : IDataLoader
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

        public ICollection<Product> retrieveProductsData()
        {
            var excelManager = new ExcelManager();
            var filePath = this.unpackDirectory + "/" + ProductFileName;

            return excelManager.ReadProductExcelFile(filePath);
        }

        public ICollection<Order> retrieveOrdersData()
        {
            var excelManager = new ExcelManager();
            var orders = new List<Order>();

            foreach (string file in Directory.GetFiles(this.unpackDirectory))
            {
                if (file.EndsWith(".xlsx"))
                {
                    orders.AddRange(excelManager.ReadOrderExcelFile(file));
                }
            }

            return orders;
        }


        public ICollection<Employee> retrieveEmployeesData()
        {
            throw new NotImplementedException();
        }
    }
}
