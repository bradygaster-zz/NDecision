using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NDecision.MVCDemo.Models;

namespace NDecision.MVCDemo.Controllers
{
    public class HomeController : Controller
    {
        const string tasks = "tasks";

        List<TaskViewModel> GetTasks()
        {
            if (Session[tasks] == null) Session[tasks] = new List<TaskViewModel>(); 
            return (List<TaskViewModel>)Session[tasks];
        }

        public ActionResult Index()
        {
            ViewBag.Tasks = this.GetTasks();
            
            if (ViewBag.Tasks.Count == 0)
            {
                ViewBag.Tasks.Add(new TaskViewModel { IsComplete = false, Description = "Something I need to do" });
                ViewBag.Tasks.Add(new TaskViewModel { IsComplete = true, Description = "Something I already did" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(TaskViewModel model)
        {
            return View();
        }
    }
}
