using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace DataTools
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
            var dbContext = new YiDbContext();
            //dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            Assert.Pass();
        }
    }
}