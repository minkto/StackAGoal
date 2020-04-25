using StackAGoal.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace StackAGoal.Core.Validation
{
    public class GoalValidDateCompleted: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var selectedGoal = (Goal)validationContext.ObjectInstance;

            if (selectedGoal.DateCompleted < selectedGoal.StartDate)
            {
                return new ValidationResult("Date Completed cannot be before the Start Date.");
            }

            return ValidationResult.Success;
        }
    }
}
