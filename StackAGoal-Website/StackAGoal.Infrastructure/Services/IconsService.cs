using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace StackAGoal.Infrastructure.Services
{
    public class IconsService : IIconsService
    {
        private readonly AppDbContext _context;
        public IconsService(AppDbContext context) 
        {
            _context = context;        
        }

        public IEnumerable<Icon> GetIcons()
        {
            return _context.Icons.ToList();
        }
    }
}
