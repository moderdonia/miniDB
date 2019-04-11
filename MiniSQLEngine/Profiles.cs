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
        Dictionary<string, string> userList;
        Dictionary<string, List<bool>> secProfiles;
        Dictionary<string, string> userSecProfiles;

        Profiles()
        {
            Allprivileges.Add("delete");
            Allprivileges.Add("insert");
            Allprivileges.Add("select");
            Allprivileges.Add("update");
        }
        

        public class Admin : Profiles
        {
            private string profileName;
            private string password;
            private List<bool> privileges;

            Admin()
            {
                profileName = "admin";
                for(int i=0; i < Allprivileges.Count; i++)
                {
                    privileges.Add(true);
                }
                password = "admin";
                userList.Add(profileName, password);
                secProfiles.Add()
            }

            private void CreateProfile(string profile,List<bool> privileges)
            {
                secProfiles.Add(profile, privileges);
            }
            private void DeleteProfile(string profile)
            {
                userList.Remove(profile);
            }

            //change privileges
            private void GivePrivileges(string profile, List<bool> privileges)
            {
                userList[profile].Clear();
                foreach(bool b in privileges)
                {
                    userList[profile].Add(b);
                }
            }
            private void RevokePrivileges(string profile, List<bool> privileges)
            {
                userList[profile].Clear();
                foreach(bool b in privileges)
                {
                    userList[profile].Add(b);
                }
            }
            private void addUser()
            {

            } 
            private void deleteUser()
            {

            }
        }
    }
}
