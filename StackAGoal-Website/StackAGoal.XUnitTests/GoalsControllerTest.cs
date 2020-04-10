using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StackAGoal.Controllers;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace StackAGoal.XUnitTests
{
    public class GoalsControllerTest
    {
        [Fact]
        public void Index_UserWithGoals_ViewResult()
        {
            // Arrange
            int expectedGoalsOfUserCount = 2;
            int userId = 1;

            var mockGoalAggegateService = new Mock<IGoalsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockGoalAggegateService.Setup(g => g.GetGoalsByUser(userId)).Returns(GetTestGoalsByUserID(userId));

            var controller = new GoalsController(mockCategoriesService.Object, mockGoalAggegateService.Object);
            var user = IdentityTestHelper.CreateIdentityUser("test@stackagoal.com","1");

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Goal>>(viewResult.ViewData.Model);
            Assert.Equal(expectedGoalsOfUserCount, model.Count());
        }

        [Fact]
        public void Index_UserWithoutGoals_ViewResult()
        {
            // Arrange
            int expectedGoalsOfUserCount = 0;
            int userId = 100;

            var mockGoalAggegateService = new Mock<IGoalsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockGoalAggegateService.Setup(g => g.GetGoalsByUser(userId)).Returns(GetTestGoalsByUserID(userId));

            var controller = new GoalsController(mockCategoriesService.Object, mockGoalAggegateService.Object);
            var user = IdentityTestHelper.CreateIdentityUser("test@stackagoal.com", "1");

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Goal>>(viewResult.ViewData.Model);
            Assert.Equal(expectedGoalsOfUserCount, model.Count());
        }


        public IEnumerable<Goal> GetTestGoalsByUserID(int userID) 
        {
            return GetTestGoals().Where(u => u.UserId == userID);
        }

        public IEnumerable<Goal> GetTestGoals() 
        {
            var goals = new List<Goal>()
            { 
                new Goal()
                { 
                    Id = 1,
                    StartDate = DateTime.Now,
                    UserId = 1,
                    Title = "Test Goal 1",
                    Description = "This is a description about Goal 1",
                    IsComplete = false
                },

                 new Goal()
                {
                    Id = 2,
                    StartDate = DateTime.Now,
                    UserId = 1,
                    Title = "Test Goal 2",
                    Description = "This is a description about Goal 2",
                    IsComplete = false
                },

                 new Goal()
                {
                    Id = 3,
                    StartDate = DateTime.Now,
                    UserId = 2,
                    Title = "Test Goal 3",
                    Description = "This is a description about Goal 3",
                    IsComplete = false
                },

                 new Goal()
                {
                    Id = 4,
                    StartDate = DateTime.Now,
                    UserId = 2,
                    Title = "Test Goal 4",
                    Description = "This is a description about Goal 4",
                    IsComplete = false
                }
            };
            return goals; 
        }
    }
}
