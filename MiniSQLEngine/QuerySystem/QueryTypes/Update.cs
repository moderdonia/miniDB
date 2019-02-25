﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class Update : SQLtype
    {
        private string table;
        //private string database;
        private string[] attb;         //columns
        private string[] conds;    //where

        bool Execute()
        {
            throw new NotImplementedException();
        }

        public Update(string table, string[] attb, string[] conds)
        {
            this.table = table;
            //this.database = database;
            this.attb = attb;
            this.conds = conds;
        }
    }
}