// File: HeadingTest.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PonyText.Processor.Misc;

namespace PonyTextTest {
    public class HeadingTest {
        HeaderCounter header = HeaderCounter.Instance;
        [Test]
        public void TestHeadingPrefix() {
            header.Count(2);
            Assert.AreEqual("0.0.1.0.0.0", header.getHeadingNumbering(5));
            header.Count(3);
            Assert.AreEqual("0.0.1.1.0.0",header.getHeadingNumbering(5));
            header.Count(4);
            header.Count(4);
            Assert.AreEqual("0.0.1.1.2.0", header.getHeadingNumbering(5));
            header.Count(5);
            header.Count(5);
            header.Count(5);
            Assert.AreEqual("0.0.1.1.2.3", header.getHeadingNumbering(5));
            header.Reset(3);
            Assert.AreEqual("0.0.1.0.0.0", header.getHeadingNumbering(5));
            header.Count(1);
            Assert.AreEqual("0.1.0.0.0.0", header.getHeadingNumbering(5));

            header.ResetAll();
            Assert.AreEqual("0.0.0.0.0.0", header.getHeadingNumbering(5));
        }

        [Test]
        public void TestHeadingCustomPrefix() {
            header.SetHeadingFormat(0, "第C章");
            header.SetHeadingFormat(1, "第C章，第C节");
            for (int i = 0; i < 32; i++) {
                header.Count(0);
            }
            header.Count(1);
            header.Count(1);
            header.Count(1);
            header.Count(1);
            Assert.AreEqual("第三十二章", header.getHeadingNumbering(0));
            Assert.AreEqual("第三十二章，第四节", header.getHeadingNumbering(1));
        }
    }
}
