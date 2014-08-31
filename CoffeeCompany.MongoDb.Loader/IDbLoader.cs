namespace CoffeeCompany.MongoDb.Loader
{
    using System.Collections.Generic;
    using Models;

    interface IDbLoader
    {
        ICollection<ClientCompany> retrieveData();
    }
}
