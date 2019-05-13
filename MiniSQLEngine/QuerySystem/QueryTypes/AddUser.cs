using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class AddUser : SQLtype
    {
        private string name;
        private string pass;
        private string sec;

        public AddUser(string name, string pass, string sec)
        {
            this.name = name;
            this.pass = pass;
            this.sec = sec;
        }
        public override string Execute(DB database)
        {
              return database.addUser(name, pass, sec);
        }
    }
}
