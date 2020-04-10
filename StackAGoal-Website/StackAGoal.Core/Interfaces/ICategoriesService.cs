using StackAGoal.Core.Models;
using System.Collections.Generic;

namespace StackAGoal.Core.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryWithIcons(int categoryId);
        Category GetCategory(int categoryId);
        void AddCategory(Category category);
        void RemoveCategory(Category category);
        int Save();
    }
}
