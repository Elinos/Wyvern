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
        private void ExtractFilesFromZip(string zipPath, string extractPath)
        {
            string zipToUnpack = zipPath;
            string unpackDirectory = extractPath;
            using (ZipFile zip1 = ZipFile.Read(zipToUnpack))
            {
                foreach (ZipEntry e in zip1)
                {
                    e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        public ICollection<ClientCompany> retrieveData()
        {
            throw new NotImplementedException();
        }
    }
}
