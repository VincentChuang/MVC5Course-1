using System.Web;
using System.Web.Mvc;

namespace MVC5Course
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());    //註冊 所有 Action 套用
        }
    }
}
