using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Color.Models;

namespace Color.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            MainStructure mt = new MainStructure();
            try
            {
                mt.Load();
                mt.oColor.Add(new ColorInfo() { Name = "", Code = "#0094ff;" });
                mt.oColor.Add(new ColorInfo() { Name = "", Code = "#b6ff00;" });
                mt.oColor.Add(new ColorInfo() { Name = "", Code = "#b6ff00;" });
                mt.oColor.Add(new ColorInfo() { Name = "", Code = "#08bdf8;" });
                mt.oColor.Add(new ColorInfo() { Name = "", Code = "#f3bf18;" });
                mt.oColor.Add(new ColorInfo() { Name = "", Code = "#f784d8;" });
                mt.oColor.Add(new ColorInfo() { Name = "", Code = "#08bdf8;" });
                ViewBag.conme = Current.DataFolder();
                mt.Save("add colors");
            }
            catch(Exception ex) {
                ViewBag.errorms = ex.Message;
            }

            return View();
        }                                                      
                                                               
        public ActionResult About()                            
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }
    }
}
