using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class CreateSP : SQLtype
    {
        private string name;
        

        public CreateSP(string name)
        {
            this.name = name;
            
        }
        public override string Execute(DB database)
        {
            return database.CreateSecProfile(name);
        }
    }
}

