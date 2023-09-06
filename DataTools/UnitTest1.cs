//#define WINDOWS_UWP
using System;
using System.IO;
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

            // Configure the database connection

            using (var dbContext = new YiDbContext(@"c:\Git\Yi\"))
            {
                // dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
                Assert.Pass();
            }
        }
    }
}