using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2MVC.Models
{
    public class Course
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "Name Must BE More Than 2 Char")]
        [MaxLength(10, ErrorMessage = "Name Must Be Less Than 15 Char")]
        [Unique]
        public string Name { get; set; }

        [Range(50, 100, ErrorMessage = "Course Degree Must be with in range 50 to 100")]

        public int Degree { get; set; }
        [Remote(action: "CheckMinDegree", controller: "Course", AdditionalFields = "Degree", ErrorMessage = "Min Degree must be less than Course Degree ")]

        public int MinDegree { get; set; }


        [ForeignKey("Department")]
        public int DepartmentId { get; set; } 
        public Department? Department { get; set; }

        public List<instructor>? Instructors { get; set; }
            public List<crsResult>? crsResults { get; set; }
    }
}
