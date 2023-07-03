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

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("fId",fId)
            };

            executeSql(sql, parameters);
        }

        public void create(CCustomer p)
        {
            string sql = @"INSERT INTO tCustomer
(fName,fPhone,fEmail,fAddress,fPassword)
VALUES
(@fName,@fPhone,@fEmail,@fAddress,@fPassword)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("fName",SqlDbType.NVarChar,50){Value = p.fName},
                new SqlParameter("fPhone",SqlDbType.NVarChar,50){Value = p.fPhone},
                new SqlParameter("fEmail",SqlDbType.NVarChar,50){Value = p.fEmail},
                new SqlParameter("fAddress",SqlDbType.NVarChar,50){Value = p.fAddress},
                new SqlParameter("fPassword",SqlDbType.NVarChar,50){Value = p.fPassword}
            };

            executeSql(sql, parameters);

        }

        private static void executeSql(string sql, SqlParameter[] parameters)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if(parameters != null) 
            {
                cmd.Parameters.AddRange(parameters);
            }
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}