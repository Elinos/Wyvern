namespace CoffeeCompany.Import.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.IO.Compression;

    using Ionic.Zip;

    using CoffeeCompany.Models;

    internal class ExcelLoader : IDbLoader<ClientCompany>
    {
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

        public ICollection<ClientCompany> retrieveData()
        {
            throw new NotImplementedException();
        }
    }
}
