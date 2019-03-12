using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;

namespace Tests
{
    [TestClass]
    public class DeleteTest
    {
        [TestMethod]
        public void Delete()
        {
            SQLParser par = new SQLParser();
            Delete sbres = (Delete)par.Parser("DELETE FROM table1 WHERE 1=1;");
            string[] a = new string[2];
            a[0] = "1=1";
            Delete sel = new Delete("table1", a);
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getTabla(), sel.getTabla());
        }
    }
}
