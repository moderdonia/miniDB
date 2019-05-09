using MiniSQLEngine.QuerySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class Profiles
    {
        private static Profiles profiles;
        DB database;
        Table tables;
        public List<string> AllPrivileges = new List<string>();
        public Dictionary<string, string> userList= new Dictionary<string, string>(); // username - password
        public Dictionary<string, Dictionary<string, List<bool>>> secProfiles = new Dictionary<string, Dictionary<string, List<bool>>>(); // secProfileName - Privileges (TableName - PrivilegeList)
        public List<bool> adminPrivileges = new List<bool>();
        public List<bool> falsePrivileges = new List<bool>();

        //Dictionary<string, string> userSecProfiles; // userName - secProfile   binds privileges with users 

        //We gonna consider userName and secProfileName must be the same.

        public static Profiles getInstance()
        {
            if (profiles == null)
            {
                profiles = new Profiles();
            }
           
            return profiles;
        }
        private Profiles()
        {
            
            AllPrivileges.Add("DELETE");
            AllPrivileges.Add("INSERT");
            AllPrivileges.Add("SELECT");
            AllPrivileges.Add("UPDATE");

            adminPrivileges.Add(true);
            adminPrivileges.Add(true);
            adminPrivileges.Add(true);
            adminPrivileges.Add(true);

            falsePrivileges.Add(false);
            falsePrivileges.Add(false);
            falsePrivileges.Add(false);
            falsePrivileges.Add(false);

            secProfiles.Add("admin",null);
            
            userList.Add("admin", "admin");
        }
        
        public void SetDB(DB db) //invoked in Program Class
        {
            database = db;
        }
        public Table getTables(string nomTable)
        {
            
            tables = database.db[nomTable];
            
            foreach (string t in tables.dc.Keys)
            {
                secProfiles["admin"].Add(t, adminPrivileges);
            }
            return tables;
        }
    }
}
