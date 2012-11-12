using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Color.Models;

namespace Color.Controllers
{
    public class testController : Controller
    {
        //
        // GET: /test/
         MainStructure mt = new MainStructure();
             
              
        public ActionResult Index()
        {
            mt=mt.Load();
            return View(mt.oColor);
        }


        public ActionResult Create()
        {
           
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(ColorInfo color)
        {
            if (ModelState.IsValid)
            {
                mt = mt.Load();
                mt.oColor.Add(color);
                mt.Save("addcolor");
                return RedirectToAction("Index");
            }

            return View(color);
        } 

    }
}
