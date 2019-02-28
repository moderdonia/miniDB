using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;


namespace Tests
{
    [TestClass]
    public class SelectTest
    {
        [TestMethod]
        public void Select()
        {
            SQLParser par = new SQLParser();
            SQLtype sbres = par.parser("SELECT * FROM table1;");
            string[] a = new string[2];
            a[0] = "*";
            Select sel = new Select("table1", a, null);
            Assert.AreEqual(sbres., sel.GetType());
            
           // SQLtype a = new Select(null,null,null);
           // Select b = new Select(null,null,null);
           // Assert.AreEqual(a.GetType().ToString(), b.GetType());
        }
    }
}