using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            string dbPath = "Empty";

#if __ANDROID__
    // Android-specific path
     dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), yiDbName);
#elif WINDOWS_UWP
    // Windows-specific path
     dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, yiDbName);
#endif
            // Configure the database connection
            Console.WriteLine($"Filename={dbPath}");

            var dbContext = new YiDbContext();
            //dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            Assert.Pass();
        }
    }
}