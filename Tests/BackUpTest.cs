using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
namespace Tests
{
    [TestClass]
    public class BackUpTest
    {
        [TestMethod]
        public void BackUp()
        {
            SQLParser par = new SQLParser();
            BackUp sbres = (BackUp)par.Parser("BACKUP DATABASE database TO DISK = 'filepath';");
            BackUp backup = new BackUp("database","filepath");
            Assert.AreEqual(sbres.GetType(), backup.GetType());
            Assert.AreEqual(sbres.getDB(), backup.getDB());
        }
    }
}
