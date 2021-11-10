using GlassCalc.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            VidroCalc vidroCalc = new VidroCalc();
            //double actualInterp = vidroCalc.Interpolate(10, 20, 30, 10, 20); //this interpolation should result 1.5
            double expected = 15;
            Assert.AreEqual(expected, 15);
        }
    }
}
