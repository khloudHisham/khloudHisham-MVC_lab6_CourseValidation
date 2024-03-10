using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace lab2MVC.Models
{
    public class UniqueAttribute : ValidationAttribute

    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ITIContext Context = new ITIContext();
            var Course = validationContext.ObjectInstance as Course;

            var DeptId = Course.DepartmentId;


            var existingCourses = Context.Course
     .Where(c => c.DepartmentId == DeptId)
     .Where(c => c.Name == value)
     .ToList();


            if (existingCourses.Count() == 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("The course name must be unique within the department.");

        }

    }

}
    

