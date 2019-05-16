using MiniSQLEngine.QuerySystem;
using MiniSQLEngine.QuerySystem.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class SQLParser
    {
        string select1;
        string select2;
        string select3;
        string select4;
        string select5;
        string select6;
        string insert1;
        string insert2;
        string insert3;
        string insert4;
        string delete;
        string update1;
        string update2;
        string dropTable;
        string dropDB;
        //string createDB;
        string backupDB;
        string createTable1;
        string createTable2;
        string login;

        //Security strings
        string create;
        string drop;
        string grant;
        string revoke;
        string addUser;
        string deleteUser;

        string createXML;
        string queryML;

        public SQLParser()
        {
            //SELECT
            //Con *:
            select1 = @"SELECT\s*(\*)\s*FROM\s*(\w+);";
            select2 = @"SELECT\s*(\*)\s*FROM\s*(\w+)\s*WHERE\s*(\w+)\s*(=|<|>)\s*([\w\']+);";
            //Con una tabla:
            select3 = @"SELECT\s*(\w+)\s*FROM\s*(\w+);";
            select4 = @"SELECT\s*(\w+)\s*FROM\s*(\w+)\s*WHERE\s*(\w+)\s*(=|<|>)\s*([\w\']+);";
            //Con mas de una tabla:
            select5 = @"SELECT\s*(\w+)(\,\s*(\w+))+\s*FROM\s*(\w+);";
            select6 = @"SELECT\s*(\w+)(\,\s*(\w+))+\s*FROM\s*(\w+)\s*WHERE\s*(\w+)\s*(=|<|>)\s*([\w\']+);";

            //Insert
            insert1 = @"INSERT\s*INTO\s*(\w+)\s*VALUES\s*\(([\w\'\s*\.\-]+)\);"; //CON TODOS SUS VALUES(1)
            insert2 = @"INSERT\s*INTO\s*(\w+)\s*VALUES\s*\(*([\w\'\s*\.\-]+)(\,\s*([\w\'\s*\.\-]+))+\);"; //(CON TODOS SUS VALUES(+1))
            insert3 = @"INSERT\s*INTO\s*(\w+)\s*\((\w+)\)\s*VALUES\s*\(([\w\'\s*\.\-]+)\);"; //(CON UN VALUE)
            insert4 = @"INSERT\s*INTO\s*(\w+)\s*\((\w+)(\,\s*(\w+))+\)\s*VALUES\s*\(([\w\'\s*\.\-]+)(\,\s*([\w\'\s*\.\-]+))+\);"; //(completa)

            //Delete
            delete = @"DELETE\s*FROM\s*(\w+)\s*WHERE\s*(\w+)\s*(=|<|>)\s*(\w+);";

            //Update
            update1 = @"UPDATE\s*(\w+)\s*SET\s*(\w+)\s*(\=)\s*(\w+)\s*WHERE\s*(\w+)\s*(=|<|>)\s*(\w+);";
            update2 = @"UPDATE\s*(\w+)\s*SET\s*(\w+)\s*(\=)\s*(\w+)(\,\s*(\w+)\s*(\=)\s*(\w+)\s*)WHERE\s*(\w+)\s*(=|<|>)\s*(\w+);"; //no traga comillas en los attbs ni espacios

            //Drop Table
            dropTable = @"DROP\s*TABLE\s*(\w+);";

            //Drop DB
            dropDB = @"DROP\s*DATABASE\s*(\w+);";

            //Create DB
            //createDB = @"CREATE\s*DATABASE\s*(\w+);";

            //Backup DB
            backupDB = @"BACKUP\s*DATABASE\s*(\w+)\s*TO\s*DISK\s*\=\s*\'([^\']+)\';";

            //Create Table
            //string createTable = @"(CREATE\s*TABLE)\s*(\w+)\s*\((\w+)\s*(INT|DOUBLE|TEXT)(\s*|\,\s*\w+\s*(INT|DOUBLE|TEXT))+)\,\s*(PRIMARY\s*KEY)\s*\((\w+)\)\,\s*(FOREIGN\s*KEY)\s*\((\w+)\)\s*REFERENCES\s*(\w+)\s*\((\w+)\);";
            createTable1 = @"CREATE\s*TABLE\s*(\w+)\s*\((\w+)\s*(INT|DOUBLE|TEXT)\);";
            createTable2 = @"CREATE\s*TABLE\s*(\w+)\s*\((\w+)\s*(INT|DOUBLE|TEXT)(\,\s*(\w+)\s*(INT|DOUBLE|TEXT))+\);";

            //Login
            login = @"(\w+),(\w+),(\w+)";

            //Create Security
            create = @"CREATE\s*SECURITY\s*PROFILE\s*(\w+);";

            //Delete Security
            drop = @"DROP\s*SECURITY\s*PROFILE\s*(\w+);";

            //Grant
            grant = @"GRANT\s*(DELETE|INSERT|SELECT|UPDATE)\s*ON\s*(\w+)\s*TO\s*(\w+);";

            //Revoke
            revoke = @"REVOKE\s*(DELETE|INSERT|SELECT|UPDATE)\s*ON\s*(\w+)\s*TO\s*(\w+);";

            //Add User
            addUser = @"ADD\s*USER\s*\(\'(\w+)\'\,\s*\'(\w+)\'\,\s*(\w+)\);";

            //Delete User
            deleteUser = @"DELETE\s*USER\s*(\w+);";

            
        }

        

        public SQLtype Parser(string query)
        {
            
            //CREATE TABLE
            Column[] ct2 = new Column[10];
            string[] ct3 = new string[10]; //*
            string ct1 = "";
            
            Match matchcreate1 = Regex.Match(query, createTable1);
            Match matchcreate2 = Regex.Match(query, createTable2);

            if (matchcreate1.Success)
            {
                ct1 = matchcreate1.Groups[1].Value;
                ct3[0] = matchcreate1.Groups[2].Value;
                //ct2[0].name = matchcreate1.Groups[2].Value;
                //ct2[0].type = matchcreate1.Groups[2].Value;

                SQLtype sentencia = new CreateTable(ct1, ct3); //tipos de momento sin implementar*

                return sentencia;

            }
            else if (matchcreate2.Success)
            {
                ct1 = matchcreate2.Groups[1].Value;
                ct3[0] = matchcreate2.Groups[2].Value; //*
                //ct2[0].name = matchcreate2.Groups[2].Value;
                //ct2[0].type = matchcreate2.Groups[2].Value;

                for (int i = 0; i < matchcreate2.Groups[5].Captures.Count; i++)
                {
                    ct3[i+1] = matchcreate2.Groups[5].Captures[i].Value; //*
                    /*ct2[i + 1].name = matchcreate2.Groups[5].Captures[i].Value;
                    ct2[i + 1].type = matchcreate2.Groups[6].Captures[i].Value;*/
                }

                SQLtype sentencia = new CreateTable(ct1, ct3);

                return sentencia;
                
            }



            //BACKUP DB
            string backupDB1 = "";
            string backupDB2 = "";

            Match matchBPDB = Regex.Match(query, backupDB);

            if (matchBPDB.Success)
            {
                backupDB1 = matchBPDB.Groups[1].Value;
                backupDB2 = matchBPDB.Groups[2].Value;

                SQLtype sentencia = new BackUp(backupDB1, backupDB2);

                return sentencia;

            }

            /*
            //CREATE DB
            string createDB1 = "";

            Match matchcDB = Regex.Match(query, createDB);

            if (matchcDB.Success)
            {
                createDB1 = matchcDB.Groups[1].Value;

                SQLtype sentencia = new CreateDB(createDB1);

                return sentencia;

            }
            */


            //UPDATE
            string[] cupdate2 = new string[10];
            string cupdate1 = "";
            string[] cupdate3 = new string[10];

            Match matchUp1 = Regex.Match(query, update1);
            Match matchUp2 = Regex.Match(query, update2);

            if (Regex.Match(query, update1).Success)
            {
                cupdate1 = matchUp1.Groups[1].Value;
                cupdate2[0] = matchUp1.Groups[2].Value;
                cupdate2[1] = matchUp1.Groups[3].Value;
                cupdate2[2] = matchUp1.Groups[4].Value;
                cupdate3[0] = matchUp1.Groups[5].Value;
                cupdate3[1] = matchUp1.Groups[6].Value;
                cupdate3[2] = matchUp1.Groups[7].Value;

                SQLtype sentencia = new Update(cupdate1, cupdate2, cupdate3);

                return sentencia;
            }
            else if (Regex.Match(query, update2).Success)
            { 
                cupdate1 = matchUp2.Groups[1].Value;
                //The first column of the set
                cupdate2[0] = matchUp2.Groups[2].Value;
                cupdate2[1] = matchUp2.Groups[3].Value;
                cupdate2[2] = matchUp2.Groups[4].Value;
                //The second column of the set
                cupdate2[3] = matchUp2.Groups[6].Value;
                cupdate2[4] = matchUp2.Groups[7].Value;
                cupdate2[5] = matchUp2.Groups[8].Value;

                cupdate3[0] = matchUp2.Groups[9].Value;
                cupdate3[1] = matchUp2.Groups[10].Value;
                cupdate3[2] = matchUp2.Groups[11].Value;

                SQLtype sentencia = new Update(cupdate1, cupdate2, cupdate3);

                return sentencia;
            }



            //DELETE
            string delete1 = "";
            string[] delete2 = new string[10];

            Match matchDel = Regex.Match(query, delete);

            if (Regex.Match(query, delete).Success)
            {
                delete1 = matchDel.Groups[1].Value;
                delete2[0] = matchDel.Groups[2].Value;
                delete2[1] = matchDel.Groups[3].Value;
                delete2[2] = matchDel.Groups[4].Value;

                SQLtype sentencia = new Delete(delete1, delete2);

                return sentencia;
            }


            //SELECT

            string[] camp1 = new string[10];
            string camp2 = "";
            string[] camp3 = new string[10];


            Match matchS1 = Regex.Match(query, select1);
            Match matchS2 = Regex.Match(query, select2);
            Match matchS3 = Regex.Match(query, select3);
            Match matchS4 = Regex.Match(query, select4);
            Match matchS5 = Regex.Match(query, select5);
            Match matchS6 = Regex.Match(query, select6);

            if (Regex.Match(query, select1).Success)
            {
                camp1[0] = matchS1.Groups[1].Value;
                camp2 = matchS1.Groups[2].Value;

                SQLtype sentencia = new Select(camp2,camp1,camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select2).Success)
            {
                camp1[0] = matchS2.Groups[1].Value;
                camp2 = matchS2.Groups[2].Value;
                camp3[0] = matchS2.Groups[3].Value;
                camp3[1] = matchS2.Groups[4].Value;
                camp3[2] = matchS2.Groups[5].Value;

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select3).Success)
            {
                camp1[0] = matchS3.Groups[1].Value;
                camp2 = matchS3.Groups[2].Value;
                
                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select4).Success)
            {
                camp1[0] = matchS4.Groups[1].Value;
                camp2 = matchS4.Groups[2].Value;
                camp3[0] = matchS4.Groups[3].Value;
                camp3[1] = matchS4.Groups[4].Value;
                camp3[2] = matchS4.Groups[5].Value;

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select5).Success)
            {

                camp1[0] = matchS5.Groups[1].Value;
                for (int i = 0; i < matchS5.Groups[3].Captures.Count; i++)
                {
                    camp1[i+1] = matchS5.Groups[3].Captures[i].Value;

                }
                camp2 = matchS5.Groups[4].Value;

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select6).Success)
            {

                camp1[0] = matchS6.Groups[1].Value;
                for (int i = 0; i < matchS6.Groups[3].Captures.Count; i++)
                {
                    camp1[i+1] = matchS6.Groups[3].Captures[i].Value;

                }
                camp2 = matchS6.Groups[4].Value;
                camp3[0] = matchS6.Groups[5].Value;
                camp3[1] = matchS6.Groups[6].Value;
                camp3[2] = matchS6.Groups[7].Value;  

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }


            //Drop table

            string campoDT1 = "";

            Match matchDT = Regex.Match(query, dropTable);

            if (matchDT.Success)
            {
                campoDT1 = matchDT.Groups[1].Value;

                SQLtype sentencia = new DropTable(campoDT1);

                return sentencia;
            }


            //Drop DB

            string campoDB1 = "";

            Match matchDB = Regex.Match(query, dropDB);

            if (matchDB.Success)
            {
                campoDB1 = matchDB.Groups[1].Value;

                SQLtype sentencia = new DropDB(campoDB1);

                return sentencia;

            }


            //Insert 

            string cinsert1;
            string[] cinsert2 = new string[10];
            string[] cinsert3 = new string[10];

            Match matchI1 = Regex.Match(query, insert1);
            Match matchI2 = Regex.Match(query, insert2);
            Match matchI3 = Regex.Match(query, insert3);
            Match matchI4 = Regex.Match(query, insert4);

            if (Regex.Match(query, insert1).Success)
            {
                cinsert1 = matchI1.Groups[1].Value;
                //cinsert3[0] = matchI1.Groups[2].Value;

                SQLtype sentencia = new Insert(cinsert1, cinsert2, cinsert3);

                return sentencia;
            }
            else if(Regex.Match(query, insert2).Success)
            {
                cinsert1 = matchI2.Groups[1].Value;
                cinsert3[0] = matchI2.Groups[2].Value;
                for (int i = 0; i < matchI2.Groups[4].Captures.Count; i++)
                {
                    cinsert3[i+1] = matchI2.Groups[4].Captures[i].Value;

                }

                SQLtype sentencia = new Insert(cinsert1, cinsert2, cinsert3);

                return sentencia;
            }
            else if (Regex.Match(query, insert3).Success)
            {
                cinsert1 = matchI3.Groups[1].Value;
                cinsert3[0] = matchI3.Groups[3].Value;
                cinsert2[0] = matchI3.Groups[2].Value;

                SQLtype sentencia = new Insert(cinsert1, cinsert2, cinsert3);

                return sentencia;
            }
            else if (Regex.Match(query, insert4).Success)
            {
                cinsert1 = matchI4.Groups[1].Value;
                cinsert3[0] = matchI4.Groups[5].Value;
                for (int i = 0; i < matchI4.Groups[7].Captures.Count; i++)
                {
                    cinsert3[i+1] = matchI4.Groups[7].Captures[i].Value;

                }
                cinsert2[0] = matchI4.Groups[2].Value;
                for (int i = 0; i < matchI4.Groups[4].Captures.Count; i++)
                {
                    cinsert2[i+1] = matchI4.Groups[4].Captures[i].Value;

                }

                SQLtype sentencia = new Insert(cinsert1, cinsert2, cinsert3);

                return sentencia;
            }

            //CREATE DB
            string createDB1 = "";
            string createDB2 = "";
            string createDB3 = "";

            Match matchcDB = Regex.Match(query, login);

            if (matchcDB.Success)
            {
                createDB1 = matchcDB.Groups[1].Value;
                createDB2 = matchcDB.Groups[2].Value;
                createDB3 = matchcDB.Groups[3].Value;
                
                SQLtype sentencia = new Login(createDB1,createDB2,createDB3);

                return sentencia;

            }

            //CREATE SECURITY PROFILE
            string security ="";

            Match matchCS = Regex.Match(query, create);

            if (matchCS.Success)
            {
                security = matchCS.Groups[1].Value;
                
                SQLtype sentencia = new CreateSP(security);

                return sentencia;

            }

            //DROP SECURITY PROFILE
            string securityD = "";

            Match matchDS = Regex.Match(query, drop);

            if (matchDS.Success)
            {
                securityD = matchDS.Groups[1].Value;

                SQLtype sentencia = new DropSP(securityD);

                return sentencia;

            }

            //GRANT
            string type = "";
            string table = "";
            string profile = "";

            Match matchG = Regex.Match(query, grant);

            if (matchG.Success)
            {
                type = matchG.Groups[1].Value;
                table = matchG.Groups[2].Value;
                profile = matchG.Groups[3].Value;

                SQLtype sentencia = new Grant(type, table, profile);

                return sentencia;

            }

            //REVOKE
            string typeR = "";
            string tableR = "";
            string profileR = "";

            Match matchR = Regex.Match(query, revoke);

            if (matchR.Success)
            {
                typeR = matchR.Groups[1].Value;
                tableR = matchR.Groups[2].Value;
                profileR = matchR.Groups[3].Value;

                SQLtype sentencia = new Login(typeR, tableR, profileR);

                return sentencia;

            }

            //ADD USER
            string user = "";
            string pass = "";
            string profileU = "";

            Match matchU = Regex.Match(query, addUser);

            if (matchU.Success)
            {
                user = matchU.Groups[1].Value;
                pass = matchU.Groups[2].Value;
                profileU = matchU.Groups[3].Value;

                SQLtype sentencia = new AddUser(user, pass, profileU);

                return sentencia;

            }

            //DELETE USER
            string userD = "";

            Match matchDU = Regex.Match(query, deleteUser);

            if (matchDU.Success)
            {
                userD = matchDU.Groups[1].Value;

                SQLtype sentencia = new DeleteUser(userD);

                return sentencia;

            }

            return null;

        }
        public string DesParser(string query)
        {

            //CreateDBXML
            string createXML = @"<Open\s*Database\=\'(\w+)\'\s*User\=\'(\w+)\'\s*Password\=\'(\w+)\'\/>;";

            //QueryXML
            string queryXML = @"<Query\>([\w+\s*\*\=]+\;)\<\/Query\>";

            string queryX = "";

            Match matchXML = Regex.Match(query, queryXML);
            Match matchXML2 = Regex.Match(query, createXML);

            if (matchXML.Success)
            {
                queryX = matchXML.Groups[1].Value;
            }
            else if (matchXML2.Success)
            {
                queryX = matchXML2.Groups[1].Value;
                queryX += ", " + matchXML2.Groups[2].Value;
                queryX += ", " + matchXML2.Groups[3].Value;
            }
            return queryX;
        }

    }
}
