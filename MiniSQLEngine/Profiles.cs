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
        DB database;
        Table tables;
        List<string> AllPrivileges = new List<string>();
        Dictionary<string, string> userList= new Dictionary<string, string>(); // username - password
        Dictionary<string, Dictionary<string, List<bool>>> secProfiles = new Dictionary<string, Dictionary<string, List<bool>>>(); // secProfileName - Privileges (TableName - PrivilegeList)
        List<bool> adminPrivileges = new List<bool>();
        List<bool> falsePrivileges = new List<bool>();

        //Dictionary<string, string> userSecProfiles; // userName - secProfile   binds privileges with users 

        //We gonna consider userName and secProfileName must be the same.

        

        public Profiles()
        {
            
            tables = database.db[database.getNameDB()];
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

            foreach(string t in tables.dc.Keys)
            {
               secProfiles["admin"].Add(t, adminPrivileges);
            }

            userList.Add("admin", "admin");
        }
        
        public void SetDB(DB db) //invoked in Program Class
        {
            database = db;
        }

            private void CreateSecProfile(string profile)
            {
                secProfiles.Add(profile, null);              
            }
            private void DeleteSecProfile(string profile)
            {
                userList.Remove(profile);
                secProfiles.Remove(profile);
            }

            //change privileges of secProfiles on Tables 
            private void GivePrivileges(string privilege,string table, string secProf)
            {
                int index = 0;
                index = AllPrivileges.IndexOf(privilege);
                secProfiles[secProf][table].Insert(index, true);
            }
            private void RevokePrivileges(string privilege, string table, string secProf)
            {
                int index = 0;
                index = AllPrivileges.IndexOf(privilege);
                secProfiles[secProf][table].Insert(index, false);
            }
            private void addUser(string userName, string pass, string secProf)
            {
                userList.Add(userName, pass);
                secProfiles.Add(userName, null);
                foreach (string t in tables.dc.Keys)
                {
                    secProfiles[userName].Add(t, falsePrivileges);
                }

            } 
            private void deleteUser(string userName)
            {
                userList.Remove(userName);
                secProfiles.Remove(userName);
            }
        
    }
}
