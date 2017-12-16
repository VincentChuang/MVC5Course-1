using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    //建立 ViewModel
    public class MBBatchUpdateVM {
        public int ProductId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
    }

    public class MBController : BaseController {

        public ActionResult Index() {
            var data = repo.Get取得所有尚未刪除的商品資料Top10();
            ViewData.Model = data;
            ViewBag.PageTitle = "商品清單";
            return View();
        }
        [HttpPost]
        public ActionResult Index(MBBatchUpdateVM[] batch) {
            if (ModelState.IsValid) {
                foreach(var item in batch) {
                    var one = repo.Find(item.ProductId);
                    one.Price = item.Price;
                    one.Active = item.Active;
                    one.Stock = item.Stock;
                }
                try {
                    repo.UnitOfWork.Commit();
                } catch (DbEntityValidationException ex) {
                    //利用DbEntityValidationException抓取ValidationErrors
                    //如果是用傳統的Exception類別，沒辦法直接在程式碼抓ValidationErrors
                    string sErrors = "";
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors) {
                        //string entityName = item.Entry.Entity.GetType().Name;
                        foreach (DbValidationError err in item.ValidationErrors) {
                            sErrors += ";錯誤欄位：" + err.PropertyName + ",錯誤原因：" + err.ErrorMessage;
                        }
                    }
                    //ViewBag["ErrorMsg"] = sErrors;  //返回驗證錯誤
                    ViewBag.ErrorMsg = sErrors;     //返回驗證錯誤
                    ViewBag.ReturnPath = "/MB/Index";     //返回驗證錯誤

                    return PartialView("JsAlertRedirect", sErrors);

                }
                return RedirectToAction("Index");
            }
            var data = repo.Get取得所有尚未刪除的商品資料Top10();
            ViewData.Model = data;
            ViewBag.PageTitle = "商品清單";
            return View();
        }

        //示範 Action 搭配 編譯 Debug/Release
        #if !DEBUG
        [NonAction]
        #endif
        public ActionResult Debug()
        {
            return View();
        }



    }
}