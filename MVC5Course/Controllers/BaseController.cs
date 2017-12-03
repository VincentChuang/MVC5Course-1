using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class BaseController : Controller
    {

        protected ProductRepository repo = RepositoryHelper.GetProductRepository();

        //阻擋 未知 的 Action，即重導向 首頁
        protected override void HandleUnknownAction(string actionName) {
            
            if( this.ControllerContext.HttpContext.Request.HttpMethod.ToUpper() == "GET") {
                this.Redirect("/").ExecuteResult(this.ControllerContext);
            } else {
                base.HandleUnknownAction(actionName);
            }
            
        }


    }
}