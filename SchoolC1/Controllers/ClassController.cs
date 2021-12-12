using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolC1.Models;

namespace SchoolC1.Controllers
{
    public class ClassController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        //GET: Class/List
        [HttpGet]
        public ActionResult List()
        {
            //Class used to get information from the Database
            ClassDataController Controller = new ClassDataController();
            IEnumerable<Class> Classes = Controller.ListClasses();

            return View(Classes);
        }

        // GET: Class/Show/{id}
        [HttpGet]
        [Route("Class/Show/{ClassId}")]
        public ActionResult Show(int id)
        {
            ClassDataController Controller = new ClassDataController();
            Class SelectedClass = Controller.ShowClass(id);
            return View(SelectedClass);
        }

    }
}