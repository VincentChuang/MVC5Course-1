﻿using MVC5Course.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [LocalOnly]     //驗證需為 本機才可執行
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ShareData]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult VT()
        {
            return PartialView("VT");
        }

        public ActionResult MetroIndex()
        {
            return View();
        }

        public ActionResult AjaxHome()
        {
            return Content(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        }

        public ActionResult TestEncode()
        {
            return View();
        }


    }
}