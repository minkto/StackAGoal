using Microsoft.EntityFrameworkCore;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace StackAGoal.Infrastructure.Services
{
    public class CategoriesService: ICategoriesService
    {
        protected readonly AppDbContext _context;
        
        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void RemoveCategory(Category category)
        {
            // This will get the goals associated with this Cateogry and set the CategoryID to null.
            _context.Goals.Where(g => g.CategoryId == category.Id).Load();
            _context.Remove(category);
        }

        public IEnumerable<Category> GetCategories() 
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == categoryId);
        }

        public Category GetCategoryWithIcons(int categoryId)
        {
            return _context.Categories.Include(i => i.Icon)
                                      .Single(c => c.Id == categoryId);
        }

        public int Save()
        {
           return  _context.SaveChanges();
        }
    }
}
