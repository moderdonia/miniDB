using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class Login : SQLtype
    {
        private string db;
        private string user;
        private string password;
        
        public override string Execute(DB database)
        {
            if (!user.Equals("admin") && !password.Equals("admin"))
            {
                return Messages.CreateDatabaseError;
            }
            else
            {
                new CreateDB(db, user, password);
                return Messages.CreateDatabaseSuccess;
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
