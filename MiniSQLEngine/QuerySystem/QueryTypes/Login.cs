using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class Login : SQLtype
    {
        private string db;
        private string user;
        private string password;
        public List<string> dbList = new List<string>();

        public override string Execute(DB database)
        {
            if (!user.Equals("admin") && !password.Equals("admin"))
            {
                return Messages.CreateDatabaseError;
            }
            else if(!dbList.Contains(db))
            {                
                new CreateDB(db, user, password);
                dbList.Add(db);
                return Messages.CreateDatabaseSuccess;
            }
            else
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            
        }
        public Login(string db,string user, string password)
        {
            this.db = db;
            this.user = user;           
            this.password = password;           
       }
    }
}
