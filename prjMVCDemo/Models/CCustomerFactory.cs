using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CCustomerFactory
    {

        public void delete(int fId)
        {
            string sql = "DELETE FROM tCustomer WHERE fId = @fId";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("fId",fId)
            };

            executeSql(sql, parameters);
        }

        public void create(CCustomer p)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            string sql = "INSERT INTO tCustomer(";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "fName,";
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "fPhone,";
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "fEmail,";
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "fAddress,";
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "fPassword,";
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
            {
                sql = sql.Trim().Substring(0,sql.Trim().Length-1);
            }
            sql += ")VALUES(";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "@fName,";
                parameters.Add(new SqlParameter("fName", SqlDbType.NVarChar, 50) { Value = p.fName });
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "@fPhone,";
                parameters.Add(new SqlParameter("fPhone", SqlDbType.NVarChar, 50) { Value = p.fPhone });
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "@fEmail,";
                parameters.Add(new SqlParameter("fEmail", SqlDbType.NVarChar, 50) { Value = p.fEmail });
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "@fAddress,";
                parameters.Add(new SqlParameter("fAddress", SqlDbType.NVarChar, 50) { Value = p.fAddress });
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "@fPassword,";
                parameters.Add(new SqlParameter("fPassword", SqlDbType.NVarChar, 50) { Value = p.fPassword });
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
            {
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            }
            sql += ")";

            executeSql(sql, parameters);

        }

        public void update(CCustomer p)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            string sql = "UPDATE tCustomer SET ";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "fName=@fName,";
                parameters.Add(new SqlParameter("fName", p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "fPhone=@fPhone,";
                parameters.Add(new SqlParameter("fPhone", p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "fEmail=@fEmail,";
                parameters.Add(new SqlParameter("fEmail", p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "fAddress=@fAddress,";
                parameters.Add(new SqlParameter("fAddress", p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "fPassword=@fPassword,";
                parameters.Add(new SqlParameter("fPassword", p.fPassword));
            }
            if(sql.Trim().Substring(sql.Trim().Length-1,1)==",")
                sql = sql.Trim().Substring(0,sql.Trim().Length-1);

            sql += " WHERE fId = @fId";
            parameters.Add(new SqlParameter("fId",p.fId));

            executeSql(sql, parameters);
        }

        public List<CCustomer> queryAll()
        {
            string sql = "SELECT * FROM tCustomer";
            List<CCustomer> datas = queryBySql(sql,null);

            return datas;
        }

        private static List<CCustomer> queryBySql(string sql, List<SqlParameter> parameters)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if(parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());
            SqlDataReader reader = cmd.ExecuteReader();

            List<CCustomer> datas = new List<CCustomer>();

            while (reader.Read())
            {
                CCustomer x = new CCustomer();
                x.fId = (int)reader["fId"];
                x.fName = reader["fName"].ToString();
                x.fPhone = reader["fPhone"].ToString();
                x.fEmail = reader["fEmail"].ToString();
                x.fAddress = reader["fAddress"].ToString();
                x.fPassword = reader["fPassword"].ToString();

                datas.Add(x);
            }

            con.Close();
            return datas;
        }

        public CCustomer queryById(int fId)
        {
            string sql = "SELECT * FROM tCustomer WHERE fId = @fId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("fId",fId));
            List<CCustomer> datas = queryBySql(sql, parameters);
            if(datas.Count == 0)
                return null;
            return datas[0];
        }

        private static void executeSql(string sql, List<SqlParameter> parameters)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if(parameters != null) 
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = "SELECT * FROM tCustomer WHERE ";
            sql += "fName LIKE @keyword ";
            sql += "OR fPhone LIKE @keyword ";
            sql += "OR fEmail LIKE @keyword ";
            sql += "OR fAddress LIKE @keyword ";
            
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("keyword", "%" + (object)keyword + "%"));
            return queryBySql(sql, parameters);
        }

        public CCustomer authenticated(string account, string password)
        {
            string sql = "SELECT * FROM tCustomer WHERE ";
            sql += "fEmail = @fEmail ";
            sql += "OR fPassword = @fPassword ";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("fEmail", account));
            parameters.Add(new SqlParameter("fPassword", password));

            List<CCustomer> customer = queryBySql(sql, parameters);
            if (customer.Count <= 0)
            {
                return null;
            }
            return customer[0];
        }
    }
}