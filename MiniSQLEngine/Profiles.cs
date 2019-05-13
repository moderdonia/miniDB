using MiniSQLEngine.QuerySystem;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MiniSQLEngine
{
    public class Profiles
    {
        private static Profiles profiles;
        DB database;
        List<Table> tables = new List<Table>();
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

            secProfiles.Add("admin", new Dictionary<string, List<bool>>());
            
            userList.Add("admin", "admin");
        }
        
        public void SetDB(DB db) //invoked in Program Class
        {
            database = db;
        }
        public void getTables(string nomTable)
        {
            
            tables.Add(database.db[nomTable]);
            
            //foreach (Table t in tables)
            //{
               // if (secProfiles["admin"].ContainsKey(nomTable))
                //{
                    secProfiles["admin"].Add(nomTable, adminPrivileges);
                //} 
            //}
        }

        public void AddProfile(string name, string password, string secProfile, string table, List<bool> booleans)
        {
            userList.Add(name, password);

            secProfiles.Add(secProfile, new Dictionary<string, List<bool>>());

            secProfiles[secProfile].Add(table, booleans);
        }

        public void SaveProfiles()
        {
            string aux = "";
            //Código para guardar todos los perfiles y sus permisos en el fichero
            foreach(KeyValuePair<string, string> user in userList)
            {
                
            }
            File.WriteAllText(@"..\..\..\Archivos\secProfiles.txt", aux);

        }
    }
}
