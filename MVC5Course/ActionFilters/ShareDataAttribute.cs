using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using System.Web.Http.Filters;    //WebAPI 專用
using System.Web.Mvc;               //MVC 專用

//virtual 要被 extends 的 方法

namespace MVC5Course.ActionFilters
{
    public class ShareDataAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //filterContext.Controller.ViewBag
            filterContext.Controller.ViewBag.Message = "從 ShareData的 ActionFilter 而來。Your application description page.";

            base.OnResultExecuting(filterContext);
        }
    }
}