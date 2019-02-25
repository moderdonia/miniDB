using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public abstract class SQLtype    
    {

        bool Execute(DB database)
        {
            return false;
        }

    }
}
