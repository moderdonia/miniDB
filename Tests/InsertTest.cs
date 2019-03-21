using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
namespace Tests
{
    [TestClass]
    public class InsertTest
    {
        [TestMethod]
        public void Insert()
        {
            SQLParser par = new SQLParser();
            Insert sbres = (Insert)par.Parser("INSERT INTO table VALUES (value1);");
            Insert sel = new Insert("table", null, null);
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getTabla(), sel.getTabla());
        }
    }
}
