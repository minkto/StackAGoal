using StackAGoal.Core.Models;
using System.Collections.Generic;

namespace StackAGoal.Core.Interfaces
{
    public interface IGoalsService
    {
        Goal GetGoal(int goalId);
        void AddNewGoal(Goal goal);
        void RemoveGoal(Goal goal);
        IEnumerable<Goal> GetGoalsByUser(int userID);
        IEnumerable<Goal> GetAllGoals();
        int Save();
    }
}
