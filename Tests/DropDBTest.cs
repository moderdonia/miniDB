using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;

namespace Tests
{
    [TestClass]
    public class DropDBTest
    {
        [TestMethod]
        public void DropDB()
        {
            SQLParser par = new SQLParser();
            DropDB sbres = (DropDB)par.Parser("DROP DATABASE db1;");
            DropDB sel = new DropDB("db1");
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getDB(), sel.getDB());
        }
    }
}
