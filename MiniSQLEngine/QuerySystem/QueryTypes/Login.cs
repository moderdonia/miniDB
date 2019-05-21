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
        Profiles prof = Profiles.getInstance();

        public override string Execute(DB database)
        {   
            
            if(user.Equals("admin") && password.Equals("admin") && !dbList.Contains(db))
            {                
                new CreateDB(db, user, password);
                database.user = user;
                dbList.Add(db);
                return Messages.CreateDatabaseSuccess;
            }
            else if(prof.userList.Keys.Contains(user) && dbList.Contains(db))
            {
                database.user = user;
                return Messages.OpenDatabaseSuccess;
            }
            else
            {
                return Messages.CreateDatabaseError;
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
