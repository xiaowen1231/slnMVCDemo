using prjMauiDemo.Models;
using prjMVCDemo.Models;
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

        public ActionResult demoForm()
        {
            ViewBag.ans = "X = ?";
            if (!string.IsNullOrEmpty(Request.Form["txtNum1"]))
            {
                double numA = Convert.ToDouble(Request.Form["txtNum1"]);
                double numB = Convert.ToDouble(Request.Form["txtNum2"]);
                double numC = Convert.ToDouble(Request.Form["txtNum3"]);
                ViewBag.numA = numA;
                ViewBag.numB = numB;
                ViewBag.numC = numC;
                double num4 = Math.Sqrt((numB * numB) - (4 *numA * numC));
                double ans1 = (-numB + num4) / (2 * numA);
                double ans2 = (-numB - num4) / (2 * numA);
                ViewBag.ans = $"X = {ans1.ToString("0.0#")} or {ans2.ToString("0.0#")}";
            }
            return View();
        }

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

        public ActionResult showById(int? id)
        {
            if (id != null)
            { 
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM tCustomer WHERE fId = " + id.ToString();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    CCustomer x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                    ViewBag.customer = x;
                }
                con.Close();
            }
            return View();
        }

        public ActionResult BindingById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM tCustomer WHERE fId = " + id.ToString();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                }
                con.Close();
            }
            return View(x);
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

            SqlCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tCustomer WHERE fId = " + id.ToString();

            SqlDataReader reader = cmd.ExecuteReader();
            string s = "沒有符合查詢條件的資料";
            if (reader.Read())
            {
                s = reader["fName"].ToString() + "/" +
                    reader["fPhone"].ToString();
            }
            con.Close();

            return s;
        }

        public string testingInsert()
        {
            CCustomer x = new CCustomer();
            x.fName = "Hank";
            //x.fPhone = "0955772889";
            x.fEmail = "Hank@gmail.com";
            //x.fAddress = "Taipei";
            x.fPassword = "1234";

            new CCustomerFactory().create(x);
            return "新增資料成功";
        }

        public string testingDelete(int? id) 
        {
            if(id == null)
            {
                return "請輸入刪除會員的Id";
            }
            new CCustomerFactory().delete((int)id);
            return "刪除資料成功";
        }

        public string testingUpdate()
        {
            CCustomer x = new CCustomer();
            x.fId = 4;
            x.fName = "Tom";
            x.fPhone = "0955772889";
            x.fEmail = "Tom@gmail.com";
            //x.fAddress = "Taipei";
            //x.fPassword = "1234";

            new CCustomerFactory().update(x);
            return "修改資料成功";
        }

        public string testingQuery() 
        {
            return "目前客戶數:" + new CCustomerFactory().queryAll().Count.ToString();
        }

        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}