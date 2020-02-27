using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI;
using UserManagementAPI.Controllers;
using UserManagementAPI.Models;
using Xunit;

namespace WebApiTest
{
    public class UserManagementTest
    {
        private UserManagementService _service;
        private UserController _controller;

        public UserManagementTest()
        {
            _service = new UserManagementService();
            _controller = new UserController(_service);
        }

        [Fact]
        public void Get_ReturnsAllItems()
        {
            //act
            var okResult = _controller.GetUsers().Result as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(7, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed()
        {
            //Act
            var notFoundResult = _controller.GetUser(123);

            //Assert
            Assert.IsType<BadRequestResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ValidIdPassed()
        {
            //Arrange
            int testValidUser = 2;

            //Act
            var okResult = _controller.GetUser(testValidUser);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ReturnCorrectUser()
        {
            //Arange
            int testValidUser = 2;
            //Act   
            var okResult = _controller.GetUser(testValidUser).Result as OkObjectResult;
            //Assert
            Assert.IsType<User>(okResult.Value);
            Assert.Equal(testValidUser, (okResult.Value as User).ID);
        }

        [Fact]
        public void AddUser_InvalidObjectPassed()
        {
            //Arrange
            var invalidUser = new User()
            {   
                
                FirstName = "Kaira"
            };
            _controller.ModelState.AddModelError("LastName", "Required");

            //Act
            var badresponse = _controller.PostUser(invalidUser);
            //Assert
            Assert.IsType<BadRequestObjectResult>(badresponse);
        }

        [Fact]
        public void AddUser_ValidUserPassed()
        {
            //Arrange
            var validUser = new User
            {
                ID = 8,
                FirstName = "Sam",
                LastName = "Doe"
            };
            //Act
            var createdResponse = _controller.PostUser(validUser);
            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void RemoveUser_NotExistingUser()
        {
            //Arrange
            int invalidId = 55;
            //Act
            var invalidResponse = _controller.DeleteUser(invalidId);
            //Assert
            Assert.IsType<NotFoundResult>(invalidResponse);
        }

        [Fact]
        public void RemoveUser_ExistingUser()
        {
            //Arrange
            int validId = 4;
            //Act
            var okResponse = _controller.DeleteUser(validId);
            //Assert
            Assert.Equal(6, _service.GetAllUsers().Count());

        }
    }
}
