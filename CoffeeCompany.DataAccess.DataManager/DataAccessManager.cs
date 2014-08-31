using CoffeeCompany.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompany.DataAccess.DataManager
{
    public class DataAccessManager
    {
        public void AddReport()
        {
            var mySqlDb = new MySQLEntitiesModel();
            Reports newReport = new Reports();
        }
    }
}
