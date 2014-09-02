namespace CoffeeCompany.Data
{
    using System;
    using System.Collections.Generic;

    using CoffeeCompany.Data.Repositories;
    using CoffeeCompany.Models;

    public class CoffeeCompanyData : ICoffeeCompanyData
    {
        private ICoffeeCompanyDbContext context;
        private IDictionary<Type, object> repositories;

        public CoffeeCompanyData()
            : this(new CoffeeCompanyDbContext())
        {
        }

        public IGenericRepository<ClientCompany> ClientCompanies
        {
            get
            {
                return this.GetRepository<ClientCompany>();
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                return this.GetRepository<Product>();
            }
        }

        public IGenericRepository<Order> Orders
        {
            get
            {
                return this.GetRepository<Order>();
            }
        }

        public CoffeeCompanyData(ICoffeeCompanyDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
