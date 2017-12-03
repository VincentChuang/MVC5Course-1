using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {

        public ActionResult Index() {
            return View();
        }

        //測試 Partial View
        public ActionResult PartialViewTest() {
            return PartialView("Index");
        }

        public ActionResult ContentTest() {
            return Content("<script>alert('OK');location.href='/';</script>");
        }
        public ActionResult ContentTest_Better() {
            return PartialView("JsAlertRedirect", "新增成功");
        }



        public ActionResult FileTest(string dl) {
            if (String.IsNullOrEmpty(dl) == true) {
                return File(Server.MapPath("~/App_Data/A01.png"), "image/png");
            } else {
                //當為下載時候，需指定檔名
                return File(Server.MapPath("~/App_Data/A01.png"), "image/png", "GreenIsland_綠島.png");
            }
        }





    }
}