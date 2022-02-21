using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookMaintainingSystem.Models
{
    public class CodeService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        public List<SelectListItem> GetBookName()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_NAME AS BOOK_NAME,
                                  BOOK_ID AS BOOK_ID 
                           FROM BOOK_DATA ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookNameCodeData(dt);

        }
        private List<SelectListItem> MapBookNameCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["BOOK_NAME"].ToString(),
                    Value = row["BOOK_ID"].ToString()
                });
            }
            return result;
        }
        public List<SelectListItem> GetBookCategory()
        {
            DataTable dt = new DataTable();
            string sql = @"Select BOOK_CLASS_ID, 
                                  BOOK_CLASS_NAME 
                           From BOOK_CLASS ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookCategoryCodeData(dt);
        }
        private List<SelectListItem> MapBookCategoryCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["BOOK_CLASS_NAME"].ToString(),
                    Value = row["BOOK_CLASS_ID"].ToString()
                });
            }
            return result;
        }

        public List<SelectListItem> GetBookKeeper()//codeTable
        {
            DataTable dt = new DataTable();
            string sql = @"Select USER_ID,
                                  USER_ENAME,
                                  USER_CNAME
                           From MEMBER_M ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookKeeperCodeData(dt);
        }
        private List<SelectListItem> MapBookKeeperCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["USER_ENAME"].ToString() + row["USER_CNAME"].ToString(),
                    Value = row["USER_ID"].ToString()
                });
            }
            return result;
        }

        public List<SelectListItem> GetBookStatus()//codeTable
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT CODE_ID,
                                  CODE_NAME 
                            FROM BOOK_CODE 
                            WHERE CODE_TYPE='BOOK_STATUS' ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookStatusCodeData(dt);
        }
        private List<SelectListItem> MapBookStatusCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CODE_NAME"].ToString(),
                    Value = row["CODE_ID"].ToString()
                });
            }
            return result;
        }
    }
}