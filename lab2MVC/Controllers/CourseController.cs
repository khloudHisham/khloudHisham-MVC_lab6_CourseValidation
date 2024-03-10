using lab2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; // Add this if it's not already included

namespace lab2MVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly ITIContext db;

        public CourseController(ITIContext context)
        {
            db = context;
        }

        // GET: Course/ShowCourses
        public IActionResult ShowCourses()
        {

            var courses = db.Course.Include(c => c.Department).ToList();

            return View(courses);
        }
        public IActionResult AddCourse()
        {
            ViewBag.Departments = db.Department.ToList();
            return View();
        }

        
        [HttpPost]
        public IActionResult SaveNew(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Add(course);
                db.SaveChanges();
                return RedirectToAction("ShowCourses");
            }

            // If ModelState is not valid, perform another action
            // For example, redirect to the AddCourse action
            return View();
        }


        //[HttpPost]
        //public IActionResult SaveNew(Course course)
        //{   
        //        db.Course.Add(course);
        //        db.SaveChanges();
        //        return RedirectToAction("ShowCourses"); // Redirect to the list of courses after adding the new course      

        //}
        public IActionResult CheckMinDegree(int Degree, int MinDegree)
        {
            if (MinDegree >= Degree)
            {
                return Json(false);
            }

            return Json(true);

        }

    }


}

