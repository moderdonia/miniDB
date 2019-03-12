using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
namespace Tests
{
    [TestClass]
    public class CreateDBTest
    {
        [TestMethod]
        public void CreateDB()
        {
            SQLParser par = new SQLParser();
            CreateDB sbres = (CreateDB)par.Parser("CREATE DATABASE database;");
            CreateDB sel = new CreateDB("database");
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getDB(), sel.getDB());
        }
    }
}
