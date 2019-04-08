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
        Profiles()
        {
            Allprivileges.Add("Access");
            Allprivileges.Add("createProfile");
            Allprivileges.Add("deleteProfile");
            Allprivileges.Add("givePivileges");
            Allprivileges.Add("revokePivileges");
            Allprivileges.Add("changeAccess");
        }
       

        class Admin : Profiles
        {
            string profileName;
            string password;
            List<bool> privileges;
            //List<DB> dbAccessList;

            Admin(string pass)
            {
                profileName = "Admin";
                for(int i=0; i < Allprivileges.Count; i++)
                {
                    privileges.Add(true);
                }
                password = pass;
            }
            
            public void CreateProfile(string profile,List<bool> privileges)
            {

            }
            public void DeleteProfile(string profile)
            {

            }
            public void GivePrivileges(string profile, List<string> privileges)
            {

            }
            public void RevokePrivileges(string profile, List<string> privileges)
            {

            }
            public void ChangeAccesList(string profile, List<DB> databases)
            {

            }
        }
        public class Client : Profiles
        {
            string profileName;
            string password;
            List<bool> privileges;
            Client(string pass)
            {
                profileName = "client";
                password = pass;
                foreach(string s in Allprivileges)
                {
                    if (s.Equals("Access"))
                    {
                        privileges.Add(true);
                    }
                    privileges.Add(false);
                }

            }
        }
    }
}
