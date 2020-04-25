using StackAGoal.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace StackAGoal.Validation
{
    public class GoalValidStartDate: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var selectedGoal = (Goal)validationContext.ObjectInstance;

            if (selectedGoal.StartDate > selectedGoal.DateCompleted) 
            {
                return new ValidationResult("Start Date cannot be after the Date Completed.");
            }

            return ValidationResult.Success;
        }
    }
}
