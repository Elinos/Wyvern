namespace CoffeeCompany.Import.DataLoaders
{
    using System.Collections.Generic;

    public interface IDbLoader<T>
    {
        ICollection<T> retrieveData();
    }
}
