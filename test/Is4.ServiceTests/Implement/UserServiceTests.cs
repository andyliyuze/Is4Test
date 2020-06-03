using Xunit;
 
using Microsoft.Extensions.DependencyInjection;
using Is4.Service.Shared;
using Is4.ServiceTests;

namespace Is4.Service.Implement.Tests
{
    public class UserServiceTests : UnitTestBase
    {
        private readonly IUserService _service;

        public UserServiceTests()
        {
            _service = ServiceProvider.GetRequiredService<IUserService>();
        }

        [Fact()]
        public void GetListTest()
        {
            var users = _service.GetList(1, 10);

            Assert.True(false, "This test needs an implementation");
        }
    }
}