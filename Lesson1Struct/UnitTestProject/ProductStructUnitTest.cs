using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ConsoleApp;
using System.Collections;

namespace UnitTestProject
{
    [TestClass]
    public class ProductStructUnitTest
    {
        private ProductStruct ps,ps1,ps2, ps3;

        [TestInitialize]
        public void Init()
        {
            ps = new ProductStruct(0, "SomeProduct1", 1.2M, 20);
            ps1 = new ProductStruct(1, "SomeProduct2", 3.2M, 30);
            ps2 = new ProductStruct(1, "SomeProduct2", 3.2M, 30);
            ps3 = new ProductStruct(1, "SomeProduct4", 22.2M, 30);
        }

        [TestMethod]
        public void CheckStructEqual()
        {
            Assert.AreNotEqual(ps, ps1,"Must be not equal");
            Assert.AreEqual(ps1, ps2, "Must be equal");
        }

        [TestMethod]
        public void CheckStructInLinq()
        {
            ProductStruct[] list = { ps, ps1, ps2 };
            var res = list.Where(x => x.Id == 0).First();
            var min = list.Min(x => x.Cost);
            Assert.AreEqual(1.2M, min, "Minimal value was counted wrong");
            Assert.AreEqual(res, ps);
        }

        [TestMethod]
        public void CheckStructHashCodes()
        {
            Hashtable ht = new Hashtable();
            var psHash = ps.GetHashCode();
            var ps1Hash = ps1.GetHashCode();
            Assert.AreNotEqual(ps1Hash, psHash, "HashCodes two different object must not be equal");

            ht.Add(psHash, ps);
            ht.Add(ps1Hash, ps1);
            
            var hashCodePs3 = ps3.GetHashCode();
            Assert.IsFalse(ht.Contains(hashCodePs3), "Must not contain ps3 hashCode");
            Assert.IsTrue(ht.Contains(ps.GetHashCode()), " Must contain ps hashCode");
        }

    }
}
