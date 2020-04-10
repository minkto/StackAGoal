using Microsoft.AspNetCore.Mvc;
using Moq;
using StackAGoal.Controllers;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StackAGoal.XUnitTests
{
    public class CategoriesControllerTest
    {
        [Fact]
        public void Index_WithCategories_ViewResult() 
        {
            // Arrange
            int categoriesCount = 4;

            var mockIconsService = new Mock<IIconsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockCategoriesService.Setup(c => c.GetCategories()).Returns(GetTestCategories());

            var controller = new CategoriesController(mockCategoriesService.Object, mockIconsService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
            Assert.Equal(categoriesCount, model.Count());
        }

        [Fact]
        public void Index_WithoutCategories_ViewResult()
        {
            // Arrange
            int categoriesCount = 0;

            var mockIconsService = new Mock<IIconsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockCategoriesService.Setup(c => c.GetCategories()).Returns(new List<Category>());

            var controller = new CategoriesController(mockCategoriesService.Object, mockIconsService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
            Assert.Equal(categoriesCount, model.Count());
        }

        public IEnumerable<Category> GetTestCategories() 
        {
            List<Category> categories = new List<Category>() 
            { 
                new Category(){Id = 0, Name = "Sports" },
                new Category(){Id = 1, Name = "Art" },
                new Category(){Id = 2, Name = "History" },
                new Category(){Id = 3, Name = "Chores" },
            };
            return categories;
        }
    }
}
