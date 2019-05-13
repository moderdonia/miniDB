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
            CreateDB sel = new CreateDB("database", "admin", "admin");
            Assert.AreEqual("database", sel.getDB());
        }
    }
}
