using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.ViewModels;
using MVC5Course.ActionFilters;
using PagedList;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        public ActionResult Index(int pageNo = 1)
        {
            var ordered = db.Product.OrderBy(p => p.ProductId);
            var data = ordered.ToPagedList(pageNo, 10);

            return View(data);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            ViewBag.OrderLines = product.OrderLine.Take(5).ToList();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Price下拉選單] //採用ActionFilter傳入 下拉選單資料
        public ActionResult Create()
        {
            //var items = new List<SelectListItem>();
            //items.Add(new SelectListItem() { Value = "0", Text = "0" });
            //items.Add(new SelectListItem() { Value = "10", Text = "10" });
            //items.Add(new SelectListItem() { Value = "20", Text = "20" });
            //items.Add(new SelectListItem() { Value = "30", Text = "30" });
            //ViewBag.Price = new SelectList(items, "Value", "Text");

            #region 產生 下拉選單 使用的資料，試改用 ActionFilter 傳入，因此下述語法不執行
            //var price_list = (from p in db.Product
            //                  select new {
            //                      Value = p.Price,
            //                      Text = p.Price
            //                  }).Distinct().OrderBy(p => p.Value);
            //ViewBag.Price = new SelectList(price_list, "Value", "Text");
            #endregion

            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Price下拉選單] //採用ActionFilter傳入 下拉選單資料
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            #region 產生 下拉選單 使用的資料，試改用 ActionFilter 傳入，因此下述語法不執行
            //var price_list = (from p in db.Product
            //                  select new {
            //                      Value = p.Price,
            //                      Text = p.Price
            //                  }).Distinct().OrderBy(p => p.Value);
            //ViewBag.Price = new SelectList(price_list, "Value", "Text");
            #endregion

            if (ModelState.IsValid) {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Price下拉選單] //採用ActionFilter傳入 下拉選單資料
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            #region 產生 下拉選單 使用的資料，試改用 ActionFilter 傳入，因此下述語法不執行
            //var price_list = (from p in db.Product
            //                  select new {
            //                      Value = p.Price,
            //                      Text = p.Price
            //                  }).Distinct().OrderBy(p => p.Value);
            //ViewBag.Price = new SelectList(price_list, "Value", "Text");
            #endregion

            return View(product);
        }


        #region Edit - Model Binding 預先載入 範例
        /*
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product) {
        ////Bind Include 代表 修改 MVC Model BIND 的特性，只允許填入該值
        ////Model Binding 過程：先寫入值->輸入驗證->模型驗證。
        //    if (ModelState.IsValid) {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}
        */
        #endregion 延遲載入
        #region Edit - Model Binding 延遲載入驗證 範例
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Price下拉選單] //採用ActionFilter傳入 下拉選單資料
        public ActionResult Edit(int id) {
            //Bind Include 代表 修改 MVC Model BIND 的特性，只允許填入該值
            //Model Binding 過程：先寫入值->輸入驗證->模型驗證。
            var product = db.Product.Find(id);

            #region 產生 下拉選單 使用的資料，試改用 ActionFilter 傳入，因此下述語法不執行
            //var price_list = (from p in db.Product
            //                  select new {
            //                      Value = p.Price,
            //                      Text = p.Price
            //                  }).Distinct().OrderBy(p => p.Value);
            //ViewBag.Price = new SelectList(price_list, "Value", "Text");
            #endregion

            //使用 Model Binding 延遲載入驗證 範例，進行 TryUpdateModel 才進行 Model Binding
            if (TryUpdateModel(product,new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" })) {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        #endregion


        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult List()
        {
            var data = from p in db.Product.Take(10)
                       select new ProductListVM() {
                           ProductName = p.ProductName,
                           ProductId = p.ProductId,
                           Price = p.Price,
                           Stock = p.Stock,
                       };

            return View(data);
        }

    }
}
