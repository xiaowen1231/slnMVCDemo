using prjMauiDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class AController : Controller
    {
        public string sayHello()
        {
            return "Hello ASP.NET MVC.";
        }

        public string lotto()
        {
            return (new CLotto()).getNumber();
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\note\01.jpg");
            Response.End();
            return "";
        }
        public string demoRequest()
        {
            string id = Request.QueryString["id"];
            if (id == "0")
                return "蘋果 加入購物車成功";

            else if (id == "1")
                return "香蕉 加入購物車成功";

            return "找不到該產品資料";

        }
        public string demoParameter(int? cid)
        {
            if (cid == 0)
                return "蘋果 加入購物車成功";

            else if (cid == 1)
                return "香蕉 加入購物車成功";

            return "找不到該產品資料";

        }

        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }

        public string queryById(int? id)
        {
            if (id == null)
            {
                return "未輸入ID";
            }
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            con.Close();
        }
        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}