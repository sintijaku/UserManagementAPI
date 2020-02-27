using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using FluentAssertions;
using UserManagementAPI;
using UserManagementAPI.Models;
using Xunit;
using UserManagementAPI.Controllers;

namespace WebApiTest
{
    public class UserControllerTest
    {
        private IUserService _userService;
        private UserController _controller;

        public UserControllerTest()
        {
            _userService = Substitute.For<IUserService>();
            _controller = new UserController(_userService);
            _userService.GetById(1).Returns(new User() { FirstName = "guven", LastName = "Aslan", ID = 1, IsActive = true });
        }

        [Fact]
        public void testtest()
        {
            //act
            var user = _controller.GetUser(1);

            //assert
            user.Should().NotBeNull();
        }

    }
}
