using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
namespace Tests
{
    [TestClass]
    public class CreateTableTest
    {
        [TestMethod]
        public void CreateTable()
        {
            SQLParser par = new SQLParser();
            CreateTable sbres = (CreateTable)par.Parser("CREATE TABLE tabla1 (edad INT);");
            string[] a = new string[2];
            a[0] = "edad";
            CreateTable sel = new CreateTable("tabla1", a);
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getTabla(), sel.getTabla());
        }
    }
}
