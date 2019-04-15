using MiniSQLEngine.QuerySystem;
using MiniSQLEngine.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class Profiles
    {
        
        List<string> Allprivileges = new List<string>();
        Dictionary<string, string> userList; // username - password
        Dictionary<string, Dictionary<Table,List<bool>>> secProfiles; // secProfileName - Privileges (Table - PrivilegeList)
        Dictionary<string, string> userSecProfiles; // userName - secProfile   binds privileges with users 


        private string profileName;
        private string password;
        Dictionary<Table, List<bool>> privileges;
        List<bool> adminPrivileges;
        //We gonna consider that de userName and secProfileName will be the same.



        Profiles()
        {
            Allprivileges.Add("delete");
            Allprivileges.Add("insert");
            Allprivileges.Add("select");
            Allprivileges.Add("update");

            for (int i = 0; i < Allprivileges.Count; i++)
            {
                foreach (Table tableName in DB.)
                {
                    privileges.Add(tableName, adminPrivileges);
                }

            }
            userList.Add("admin", "admin");
            secProfiles.Add("admin", privileges);
            userSecProfiles.Add("admin",)
        }
        


            private void CreateProfile(string profile)
            {
                secProfiles.Add(profile, null);              
            }
            private void DeleteProfile(string profile)
            {
                secProfiles.Remove(profile);
            }

            //change privileges of secProfiles on Tables 
            private void GivePrivileges(string profile, List<bool> privileges,Table table)
            {
                secProfiles[profile].Clear();
                secProfiles[profile].Add(table,privileges);
            }
            private void RevokePrivileges(string profile, List<bool> privileges, Table table)
            {
                secProfiles[profile].Clear();
                secProfiles[profile].Add(table, privileges);
            }
            private void addUser(string userName, string pass, string secProf)
            {
                userList.Add(userName, pass);
                userSecProfiles.Add(userName, secProf);

            } 
            private void deleteUser(string userName)
            {
                userList.Remove(userName);
            }
        
    }
}
