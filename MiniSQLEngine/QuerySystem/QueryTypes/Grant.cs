using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class Grant : SQLtype
    {
        private string type;
        private string table;
        private string profiles;

        public Grant(string type, string table, string profiles)
        {
            this.type = type;
            this.table = table;
            this.profiles = profiles;
        }
        public override string Execute(DB database)
        {
            return database.GivePrivileges(type, table, profiles);
        }
    }
}

