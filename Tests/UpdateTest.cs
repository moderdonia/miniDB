using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
namespace Tests
{
    [TestClass]
    public class UpdateTest
    {
        [TestMethod]
        public void Update()
        {
            SQLParser par = new SQLParser();
            Update sbres = (Update)par.Parser("UPDATE table SET column1=value1 WHERE 1=1;");
            string[] a = new string[2];
            a[0] = "*";
            Update sel = new Update("table", a, null);
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getTabla(), sel.getTabla());
        }
    }
}
