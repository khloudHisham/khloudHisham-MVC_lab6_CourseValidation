using lab2MVC.Models;
using lab2MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System;

namespace lab2MVC.Controllers
{
    public class TraineCRSController : Controller
    {
        private readonly ITIContext context;

        public TraineCRSController(ITIContext context)
        {
            this.context = context;
        }
        public IActionResult ShowCourseResult(int id)
        {
            // Step 2: Retrieve Course Result
            var courseResult = context.crsResult
                .Include(cr => cr.Course)
                .Include(cr => cr.Trainee)
                .FirstOrDefault(cr => cr.CourseId == id);

            if (courseResult == null)
            {
                return NotFound(); // Course result not found
            }

            // Step 3: Check Trainee's Degree
            var traineeDegree = courseResult.degree;

            // Step 4: Set Color
            string color = traineeDegree >= courseResult.Course.MinDegree ? "green" : "red";

            // Step 5: Create View Model
            var viewModel = new CourseResultViewModel
            {
                CourseName = courseResult.Course.Name,
                TraineeName = courseResult.Trainee.Name,
                Degree = traineeDegree,
                Color = color
            };

            // Step 6: Return View
            return View(viewModel);
        }
        public IActionResult ShowTraineeResult(int id)
        {
            // Step 2: Retrieve Trainee's Course Results
            var courseResults = context.crsResult
                .Include(cr => cr.Course)
                .Where(cr => cr.TraineeId == id)
                .ToList();

            if (courseResults.Count == 0)
            {
                return NotFound(); // No course results found for the trainee
            }

            // Step 3-4: Check Trainee's Degrees in Courses and Set Colors
            var courseResultsViewModel = new List<CourseResultViewModel>();
            foreach (var courseResult in courseResults)
            {
                var color = courseResult.degree >= courseResult.Course.MinDegree ? "green" : "red";
                courseResultsViewModel.Add(new CourseResultViewModel
                {
                    CourseName = courseResult.Course.Name,
                    Degree = courseResult.degree,
                    Color = color
                });
            }

            // Step 5: Create View Model
            var viewModel = new TraineeResultViewModel
            {
                TraineeId = id,
                CourseResults = courseResultsViewModel
            };

            // Step 6: Render the View
            return View(viewModel);
        }
        public IActionResult TraineeCourseAndGradWithVM(int id, int CrsId)
        {
            var Trainee = context.Trainee.FirstOrDefault(t => t.Id == id);
            var course = context.Course.FirstOrDefault(c => c.Id == CrsId);

            if (Trainee == null || course == null)
            {
                return NotFound();
            }
            Course crs = new Course();
            Trainee tr = new Trainee();
            crsResult crRes = context.crsResult.FirstOrDefault(n => n.TraineeId == id);

            var vm = new TraineCourseDegreeViewModel();

            vm.TraineeId = Trainee.Id;
            vm.TraineeName = Trainee.Name;
            vm.CourseName = course.Name;
            vm.CourseDegree = course.Degree;
            vm.Degree = crRes.degree;
            if (vm.Degree < course.MinDegree)
            {
                vm.color = "red";
            }
            else
            {
                vm.color = "green";
            }

            return View("TraineeCourseAndGradWithVM", vm);
        }

    }
}
