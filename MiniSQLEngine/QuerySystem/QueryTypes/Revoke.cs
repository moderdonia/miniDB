using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class Revoke : SQLtype
    {
      
        private string type;
        private string table;
        private string profiles;

        public Revoke(string type, string table, string profiles)
        {
            this.type = type;
            this.table = table;
            this.profiles = profiles;
        }
        public override string Execute(DB database)
        {
            return database.RevokePrivileges(type, table, profiles);
        }
    }
}

