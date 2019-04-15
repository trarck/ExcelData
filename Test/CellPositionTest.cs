using NUnit.Framework;
using TK.ExcelData;

namespace Tests
{
    public class CellPositionTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            CellPosition cp = ReadLinkHelper.GetCellPosition("A1B2");
            Assert.AreEqual(cp.colStart, 0);
            Assert.AreEqual(cp.colEnd, 1);
            Assert.AreEqual(cp.rowStart, 0);
            Assert.AreEqual(cp.rowEnd, 1);
        }

        [Test]
        public void Test2()
        {
            CellPosition cp = ReadLinkHelper.GetCellPosition("A1,B2");
            Assert.AreEqual(cp.colStart, 0);
            Assert.AreEqual(cp.colEnd, 1);
            Assert.AreEqual(cp.rowStart, 0);
            Assert.AreEqual(cp.rowEnd, 1);
        }

        [Test]
        public void Test3()
        {
            CellPosition cp = ReadLinkHelper.GetCellPosition("1B2");
            Assert.AreEqual(cp.colStart,0);
            Assert.AreEqual(cp.colEnd, 1);
            Assert.AreEqual(cp.rowStart, 0);
            Assert.AreEqual(cp.rowEnd, 1);
        }

        [Test]
        public void Test4()
        {
            CellPosition cp = ReadLinkHelper.GetCellPosition("1,B2");
            Assert.AreEqual(cp.colStart, 0);
            Assert.AreEqual(cp.colEnd, 1);
            Assert.AreEqual(cp.rowStart, 0);
            Assert.AreEqual(cp.rowEnd, 1);
        }

        [Test]
        public void Test5()
        {
            CellPosition cp = ReadLinkHelper.GetCellPosition("1B");
            Assert.AreEqual(cp.colStart, 0);
            Assert.AreEqual(cp.colEnd, 1);
            Assert.AreEqual(cp.rowStart, 0);
            Assert.AreEqual(cp.rowEnd, -1);
        }

        [Test]
        public void Test6()
        {
            CellPosition cp = ReadLinkHelper.GetCellPosition(",B");
            Assert.AreEqual(cp.colStart, 0);
            Assert.AreEqual(cp.colEnd, 1);
            Assert.AreEqual(cp.rowStart, 0);
            Assert.AreEqual(cp.rowEnd, -1);
        }

        [Test]
        public void Test7()
        {
            CellPosition cp = ReadLinkHelper.GetCellPosition("C4F8");
            Assert.AreEqual(cp.colStart, 2);
            Assert.AreEqual(cp.colEnd, 5);
            Assert.AreEqual(cp.rowStart,3);
            Assert.AreEqual(cp.rowEnd, 7);
        }
    }
}