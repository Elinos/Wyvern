namespace CoffeeCompany.Import.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CoffeeCompany.Models;

    internal class ExcelLoader : IDbLoader<ClientCompany>
    {
        public ICollection<ClientCompany> retrieveData()
        {
            throw new NotImplementedException();
        }
    }
}
