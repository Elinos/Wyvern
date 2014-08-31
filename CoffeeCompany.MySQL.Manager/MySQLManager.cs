using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CoffeeCompany.MySQL.Manager
{
    public class MySQLManager
    {
        MySqlConnection dbCon = new MySqlConnection(@"Server=localhost;Port=3306; Database=books;Uid=root;Pwd=test;");
    }
}
