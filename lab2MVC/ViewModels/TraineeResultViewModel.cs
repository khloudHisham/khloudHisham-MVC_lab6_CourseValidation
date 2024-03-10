using System.Collections.Generic;

namespace lab2MVC.ViewModels
{
    public class TraineeResultViewModel
    {
        // Property to hold the trainee ID
        public int TraineeId { get; set; }

        // Property to hold the list of course results
        public List<CourseResultViewModel> ? CourseResults  { get; set; }
    }
}
