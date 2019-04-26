using NUnit.Framework;
using TK.ExcelData;
using TK.Excel;

namespace Tests
{
    public class TypeInfoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInt()
        {
            TypeInfo tInfo = TypeInfo.Parse("Int");
            Assert.AreEqual(tInfo.ToString(), "int");

            tInfo = TypeInfo.Parse("int");
            Assert.AreEqual(tInfo.ToString(), "int");
        }
        [Test]
        public void TestLong()
        {
            TypeInfo tInfo = TypeInfo.Parse("Long");
            Assert.AreEqual(tInfo.ToString(), "long");

            tInfo = TypeInfo.Parse("long");
            Assert.AreEqual(tInfo.ToString(), "long");
        }

        [Test]
        public void TestFloat()
        {
            TypeInfo tInfo = TypeInfo.Parse("Float");
            Assert.AreEqual(tInfo.ToString(), "float");

            tInfo = TypeInfo.Parse("float");
            Assert.AreEqual(tInfo.ToString(), "float");
        }

        [Test]
        public void TestDouble()
        {
            TypeInfo tInfo = TypeInfo.Parse("Double");
            Assert.AreEqual(tInfo.ToString(), "double");

            tInfo = TypeInfo.Parse("double");
            Assert.AreEqual(tInfo.ToString(), "double");
        }

        [Test]
        public void TestString()
        {
            TypeInfo tInfo = TypeInfo.Parse("String");
            Assert.AreEqual(tInfo.ToString(), "string");

            tInfo = TypeInfo.Parse("string");
            Assert.AreEqual(tInfo.ToString(), "string");
        }
        [Test]
        public void TestBool()
        {
            TypeInfo tInfo = TypeInfo.Parse("Bool");
            Assert.AreEqual(tInfo.ToString(), "bool");

            tInfo = TypeInfo.Parse("bool");
            Assert.AreEqual(tInfo.ToString(), "bool");
        }

        [Test]
        public void TestGinericList()
        {
            TypeInfo tInfo = TypeInfo.Parse("List<int>");
            Assert.AreEqual(tInfo.ToString(), "List<int>");

            tInfo = TypeInfo.Parse("List<bool>");
            Assert.AreEqual(tInfo.ToString(), "List<bool>");


            tInfo = TypeInfo.Parse("List<string>");
            Assert.AreEqual(tInfo.ToString(), "List<string>");
        }

        [Test]
        public void TestGinericMore()
        {
            TypeInfo tInfo = TypeInfo.Parse("List<Dictionary<string,List<Dictionary<int,string>>>>");
            Assert.AreEqual(tInfo.ToString(), "List<Dictionary<string,List<Dictionary<int,string>>>>");

            tInfo = TypeInfo.Parse("list<dictionary<String,List<dictionary<Int,string>>>>");
            Assert.AreEqual(tInfo.ToString(), "List<Dictionary<string,List<Dictionary<int,string>>>>");
        }
    }
}