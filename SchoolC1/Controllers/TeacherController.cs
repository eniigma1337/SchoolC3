using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolC1.Models;
using System.Diagnostics;
using System.Globalization;
namespace SchoolC1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/SearchResult
        public ActionResult SearchResult(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Class> TeacherXClasses = controller.FindTeachersClass(id);
            Teacher TeacherX = controller.FindTeacher(id);
            //MAKE A TUPLE TO USE 2 MODELS FOR OUR VIEW
            return View(Tuple.Create (TeacherX, TeacherXClasses));
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //GET : /Teacher/Add
        public ActionResult Add()
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> allcourses = controller.ListClasses();
            return View(allcourses);
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime? TeacherHire, Decimal TeacherSalary = 0)
        {
            List<string> errors = new List<string>();
            //change the date format to match with database
            CultureInfo canada = new CultureInfo("en-CA");
            // Server side validation
            
            if (string.IsNullOrEmpty(TeacherHire?.ToString(canada)))
            {
                string dateString = TeacherHire?.ToString(canada);
                errors.Add("Please enter a date!");
            }
            // Validate TeacherFname
            if (string.IsNullOrEmpty(TeacherFname))
            {
                errors.Add("Please enter a First Name!");
            }
            // Validate TeacherLname
            if (string.IsNullOrEmpty(TeacherLname))
            {
                errors.Add("Please enter a Last Name!");
            }
            // Validate EmployeeNumber
            if (string.IsNullOrEmpty(EmployeeNumber))
            {
                errors.Add("Please enter an Employee Number!");
            }
            //Validate TeacherSalary
            if (TeacherSalary == 0)
            {
                errors.Add("Please enter a Salary!");
            }

            if (errors.Count > 0)
            {
                return View("AddError", errors);
            }
            else
            {
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.TeacherHire = TeacherHire;
                NewTeacher.TeacherSalary = TeacherSalary;

                TeacherDataController controller = new TeacherDataController();
                controller.AddTeacher(NewTeacher);

                return RedirectToAction("List");
            }
        }
    }
}