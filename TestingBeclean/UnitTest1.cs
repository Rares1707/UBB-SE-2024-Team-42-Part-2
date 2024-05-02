using SuperbetBeclean.Model;

namespace TestingBeclean
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {  
            Assert.Pass();
        }

        [Test]
        public void TestIfProjectsAreReferencedCorrectly()
        {
            Font font = new Font();
            Font font1 = new Font();
            Assert.That(font.FontID, Is.EqualTo(font1.FontID));
        }
    }
}