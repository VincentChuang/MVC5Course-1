using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {

        public ActionResult Index() {
            return View();
        }

        //測試 Partial View
        public ActionResult PartialViewTest() {
            return PartialView("Index");
        }


        #region 實做 彈出Alert提示後，指定轉向首頁，兩種寫法

        //不好的寫法，因在 Controller 中 撰寫 html code
        public ActionResult ContentTest() {
            return Content("<script>alert('OK');location.href='/';</script>");
        }
        //較好的寫法，指定 PartialView 且 在 Shared 中 撰寫共用 JsAlertRedirect.cshtml
        public ActionResult ContentTest_Better() {
            return PartialView("JsAlertRedirect", "新增成功");
        }

        #endregion


        public ActionResult FileTest(string dl) {
            if (String.IsNullOrEmpty(dl) == true) {
                //File Result，第三參數未指定，代表 透過 browser 呈現
                return File(Server.MapPath("~/App_Data/A01.png"), "image/png");
            } else {
                //當為下載時候，需指定檔名
                //File Result，第三參數有指定，代表下載該指定檔名檔案
                return File(Server.MapPath("~/App_Data/A01.png"), "image/png", "GreenIsland_綠島.png");
            }
        }


        public ActionResult JsonTest() {
            var data = from p in repo.All().Take(5)
                       select new {
                           p.ProductId,
                           p.ProductName,
                           p.Price
                       };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RedirectTest() {
            return RedirectToAction("FileTest",new { dl = 1 }) ;
        }
        public ActionResult RedirectTest2() {
            return RedirectToRoute(new { Controller = "Home", Action = "About", id = "123" });
        }









    }
}