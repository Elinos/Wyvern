namespace CoffeeCompany.Import.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.IO.Compression;

    using Ionic.Zip;

    using CoffeeCompany.Models;

    internal class ExcelLoader
    {
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
            throw new NotImplementedException();
        }

        public ICollection<Product> retrieveProductData()
        {
            throw new NotImplementedException();
        }
    }
}
