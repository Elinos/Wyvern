namespace CoffeeCompany.MongoDb.Loader
{
    using System.Collections.Generic;
    using Models;

    interface IMongoDbLoader
    {
        ICollection<ClientCompany> ImportCompanies();
    }
}
