using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using p.Database.Concrete.Entities;
using System.Linq;

namespace p.WebUI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ICollection<TextWatermark> wMarks = new List<TextWatermark> { new TextWatermark { Id = 1 }, new TextWatermark { Id = 1 } };
            ICollection<ImageWatermark> iMarks = new List<ImageWatermark> { new ImageWatermark { Id = 1 }, new ImageWatermark { Id = 1 } };
            var p = new Photographer();
            p.TextWatermarks = wMarks;
            p.ImageWatermarks = iMarks;
            Assert.AreEqual(4, p.Watermarks.Count());
            foreach (var m in p.Watermarks)
            {
                Assert.AreEqual(1, m.Id);
            }
        }
    }
}
