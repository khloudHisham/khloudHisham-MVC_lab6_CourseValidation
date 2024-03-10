using lab2MVC.Models;

namespace lab2MVC.ViewModels
{
    public class InstractorNewListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Image { get; set; }
        public int CourseId { get; set; }
        public int DeptId { get; set; }
        public List<Course> CrsList { get; set; }
        public List<Department> DeptList { get; set; }
    }
}
