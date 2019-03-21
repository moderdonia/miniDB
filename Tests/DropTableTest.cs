using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;

namespace Tests
{
    [TestClass]
    public class DropTableTest
    {
        [TestMethod]
        public void DropTable()
        {
            SQLParser par = new SQLParser();
            DropTable sbres = (DropTable)par.Parser("DROP TABLE table1;");
            DropTable sel = new DropTable("table1");
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getTabla(), sel.getTabla());
        }
    }
}
