using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class BackUp : SQLtype
    {
        private string database;
        private string filepath;

        public override string Execute(DB database)
        {
            throw new NotImplementedException();
        }

        public BackUp(string database, string filepath)
        {
            this.database = database;
            this.filepath = filepath;
        }
        public string getDB()
        {
            return database;
        }
        public string getFile()
        {
            return filepath;
        }
    }
}
