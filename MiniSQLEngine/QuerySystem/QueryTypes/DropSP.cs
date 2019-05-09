using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class DropSP : SQLtype
    {
        private string name;
        
        public DropSP(string name)
        {
            this.name = name;

        }
        public override string Execute(DB database)
        {
            return database.DeleteSecProfile(name);
        }
    }
}

