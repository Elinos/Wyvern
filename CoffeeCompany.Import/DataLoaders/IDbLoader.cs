namespace CoffeeCompany.Import.DataLoaders
{
    using System.Collections.Generic;

    internal interface IDbLoader<T>
    {
        ICollection<T> retrieveData();
    }
}
