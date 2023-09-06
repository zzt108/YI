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
        public void CreateOpen_Database()
        {
            // Configure the database connection
//            using var dbContext = new YiDbContext(@"c:\Git\Yi\");
            using var dbContext = new YiDbContext();
            dbContext.Database.Migrate();
        }

        [Test]
        public void CreateData()
        {
            // Configure the database connection
            //            using var dbContext = new YiDbContext(@"c:\Git\Yi\");
            using var dbContext = new YiDbContext();
            dbContext.Database.Migrate();
        }
    }
}