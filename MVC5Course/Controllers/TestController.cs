using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC5Course.Models;

using Omu.ValueInjecter;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {   

        #region Index - 列表
        public ActionResult Index()
        {
            //var data = from p in db.Product.Where(x => x.isDelete == false).Take(10)
            //           select p;                //所得所有資料

            #region 保哥 寫法
            //var data = from p in db.Product
            //           where p.isDelete == false
            //           select p;      //所得所有資料
            #endregion

            //var repo = new ProductRepository();
            //repo.UnitOfWork = new EFUnitOfWork();

            //var data = repo.All().Where(p => p.isDelete == false);

            var data = repo.Get取得所有尚未刪除的商品資料Top10();

            return View(data);
        }
        #endregion

        #region Create - 建立
        public ActionResult Create()
        {
            return View();
        }
        //打 mvcp 即可出現
        [HttpPost]
        public ActionResult Action(Product data)    
        {
            if (ModelState.IsValid) {
                
                //db.Product.Add(data);
                //db.SaveChanges();

                repo.Add(data);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            return View(data);
        }
        #endregion

        #region Edit - 編輯
        public ActionResult Edit(int id)
        {
            //var item = db.Product.Find(id);

            var item = repo.Find(id);

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(int id , Product data)
        {
            
            if (ModelState.IsValid) {

                //var updData = db.Product.Find(id);

                var updData = repo.Find(id);

                if (updData != null) {
                    //updData = data;
                    updData.ProductId = id;
                    updData.ProductName = data.ProductName;
                    updData.Active = data.Active;
                    updData.Price = data.Price;
                    updData.Stock = data.Stock;
                    updData.isDelete = false;

                    //ValueInjecter 套件 ==> 
                    //updData.inJectFrom(data);
                }

                //int iFect = db.SaveChanges();
                
                repo.UnitOfWork.Commit();

                TempData["ProductItem"] = updData;

                return RedirectToAction("Index");
            }
            return View(data);
        }
        #endregion

        #region Delete - 刪除
        public ActionResult Delete(int id) {

            #region 原先寫法，真的刪除資料
            //var listOrderLine = db.OrderLine.Where(x => x.ProductId == id).ToList();
            //if(listOrderLine!=null && listOrderLine.Count > 0) {
            //    db.OrderLine.RemoveRange(listOrderLine);
            //}
            //var delP = db.Product.Where(x => x.ProductId == id).FirstOrDefault();
            //if (delP != null) {
            //    db.Product.Remove(delP);
            //}
            //db.SaveChanges();
            //return RedirectToAction("Index");
            #endregion

            //不同 Repository 共用 同一個 repo Connection
            //var olRepo = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
            //olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));

            //不同 Repository 共用 同一個 repo Connection
            //var olRepo = new OrderLineRepository();
            //olRepo.UnitOfWork = repo.UnitOfWork;
            //olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));

            var delP = repo.Find(id);
            repo.Delete(delP);

            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }
        #endregion

        #region Details - 顯示明細
        public ActionResult Details(int id) {

            //var detailP = db.Product.Where(x => x.ProductId == id).FirstOrDefault();

            var detailP = repo.Find(id);
            if (detailP == null) {  //當查無資料，返回列表頁
                return RedirectToAction("Index");
            }
            return View(detailP);
        }
        #endregion


    }
}