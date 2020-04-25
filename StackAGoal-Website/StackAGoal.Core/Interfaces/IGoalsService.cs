using StackAGoal.Core.Models;
using System;
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
        bool IsGoalCompleted(bool isMarkedComplete, DateTime? dateCompleted);
        int Save();
    }
}
