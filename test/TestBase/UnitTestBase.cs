using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
namespace TestBase
{
    public class UnitTestBase
    {
        public UnitTestBase() 
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
        }

        [Fact]
        public void Test1()
        {
           
        }
    }
}
