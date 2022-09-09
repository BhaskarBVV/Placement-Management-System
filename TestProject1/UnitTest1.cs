using Placement_Management_System;
using Placement_Management_System.Util;
using System.Web;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateRollNumber_Exists_False()
        {
            var res = Utility.ValidateRollNumber(192);
            if(res==false)
            {
                Assert.IsFalse(res);
            }
            else
            {
                Assert.IsTrue(res); 
            }
        }


        [TestMethod]
        public void ValidateRollNumber_Exists_True()
        {
            var res = Utility.ValidateRollNumber(191);
            if (res == false)
            {
                Assert.IsFalse(res);
            }
            else
            {
                Assert.IsTrue(res);
            }
        }


        [TestMethod]
        public void GetSqlCommand_CorrectSqlArg_String()
        {
            var res = Utility.GetSqlCommand("SelectAll");
            Assert.AreEqual(res, "Select * from Student;");
        }


        [TestMethod]
        public void GetSqlCommand_InCorrectSqlArg_String()
        {
            var res = Utility.GetSqlCommand("Hello");
            Assert.AreEqual(res, "No such Sql command found");
        }


        [TestMethod]
        public void GetSqlCommand_InCorrectSqlNULL_String()
        {
            var res = Utility.GetSqlCommand("");
            Assert.AreEqual(res, "Invalid Command");
        }
    }
}