using Microsoft.EntityFrameworkCore;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackAGoal.Infrastructure.Repositories
{
    public class GoalsService: IGoalsService
    {
        protected AppDbContext _context;

        public GoalsService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void AddNewGoal(Goal goal) 
        {
            _context.Add(goal);
        }

        public void RemoveGoal(Goal goal)
        {
            _context.Goals.Remove(goal);
        }

        public Goal GetGoal(int goalId)
        {
            return _context.Goals.SingleOrDefault(g => g.Id == goalId);
        }

        public IEnumerable<Goal> GetGoalsByUser(int userID) 
        {
            var goal = _context.Goals
                .Include(g => g.Category)
                .Where(g => g.UserId == userID)
                .ToList();

            return goal;
        }

        public bool IsGoalCompleted(bool isMarkedComplete,DateTime? dateCompleted)
        {
            bool result = false;
            
            if (isMarkedComplete) 
            {
                result =  true;
            }
            else if (dateCompleted.HasValue)
            {
                result = true;
            }

            return result;
        }

        public IEnumerable<Goal> GetAllGoals()
        {
            var goals = _context.Goals.Include(g => g.Category);
            return goals;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
