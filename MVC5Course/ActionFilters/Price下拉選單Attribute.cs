using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
    public class Price下拉選單Attribute : ActionFilterAttribute
    {
        private FabricsEntities db = new FabricsEntities();

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            #region 產生 下拉選單 使用的資料
            var price_list = (from p in db.Product
                              select new
                              {
                                  Value = p.Price,
                                  Text = p.Price
                              }).Distinct().OrderBy(p => p.Value);
            filterContext.Controller.ViewBag.Price = new SelectList(price_list, "Value", "Text");
            #endregion

            base.OnResultExecuting(filterContext);
        }
        
    }
}