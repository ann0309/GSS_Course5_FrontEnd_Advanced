using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace bookMaintainingSystem.Models
{
    public class Detail : Controller
    {
        // GET: BookService
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        public ActionResult Index()
        {
            return View();
        }


        //拿到被按下的那筆資料的 bookid,
        public Models.BookEditArg GetBookByID(string id)
        {
            DataTable dt = new DataTable();
            string sql = @"	SELECT
                                bd.BOOK_ID,
	                            bd.BOOK_NAME,
	                            bd.BOOK_AUTHOR,
	                            bd.BOOK_PUBLISHER,
	                            bd.BOOK_NOTE,
	                            bd.BOOK_BOUGHT_DATE AS BOOK_BOUGHT_DATE,
	                            bc.BOOK_CLASS_NAME,
	                            bc1.CODE_NAME,
	                            mm.USER_ENAME
                            FROM BOOK_DATA AS bd
                            LEFT JOIN BOOK_CLASS AS bc
	                            ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
                            LEFT JOIN BOOK_CODE AS bc1
	                            ON bd.BOOK_STATUS = bc1.CODE_ID
		                            AND bc1.CODE_TYPE_DESC = '書籍狀態'
                            LEFT JOIN MEMBER_M AS mm
	                            ON bd.BOOK_KEEPER = mm.USER_ID
                            WHERE BOOK_ID=@BookID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", id));//@BookID對到sq語法裡的where的@BookID
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToModel(dt);
        }
        private Models.BookEditArg MapBookDataToModel(DataTable bookData)
        {
            Models.BookEditArg result = new BookEditArg();
            foreach (DataRow row in bookData.Rows)
            {
                result = new BookEditArg()
                {
                    BookID = (int)row["BOOK_ID"],
                    BookName = row["BOOK_NAME"].ToString(),
                    BookAuthor = row["BOOK_AUTHOR"].ToString(),
                    BookPublisher = row["BOOK_PUBLISHER"].ToString(),
                    BookContent = row["BOOK_NOTE"].ToString(),
                    BookBoughtDate = (DateTime)row["BOOK_BOUGHT_DATE"],
                    BookCategoryID = row["BOOK_CLASS_NAME"].ToString(),
                    BookStatus = row["CODE_NAME"].ToString(),
                    BookKeeper = row["USER_ENAME"].ToString()
                };
            }
            return result;
        }
        public List<SelectListItem> GetBookCategory(string id)
        {
            DataTable dt = new DataTable();
            string sql = @"		Select  bcl.BOOK_CLASS_ID, bcl.BOOK_CLASS_NAME 
                           From BOOK_CLASS AS bcl
						   LEFT JOIN BOOK_DATA AS bd
						   ON bcl.BOOK_CLASS_ID = bd.BOOK_CLASS_ID
						   WHERE bd.BOOK_ID=@Book_ID

                            ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BookID", id));
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
                    Value = row["BOOK_CLASS_ID"].ToString(),
                });
            }
            return result;
        }

        public List<SelectListItem> GetBookStatus(string id)//codeTable
        {
            DataTable dt = new DataTable();
            string sql = @"	SELECT CODE_ID,CODE_NAME 
							FROM BOOK_CODE							
							WHERE CODE_TYPE='BOOK_STATUS '  
							 ";

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
                    Text = row["CODE_ID"].ToString() + '-' + row["CODE_NAME"].ToString(),
                    Value = row["CODE_ID"].ToString()
                });
            }
            return result;
        }
        public List<SelectListItem> GetBookKeeper(string id)//codeTable
        {
            DataTable dt = new DataTable();
            string sql = @"Select M.USER_ID,M.USER_ENAME,M.USER_CNAME
                           From DBO.MEMBER_M AS M			   
                            ";

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
                    Text = row["USER_CNAME"].ToString() + '-' + row["USER_ENAME"].ToString(),
                    Value = row["USER_ID"].ToString()
                });
            }
            return result;
        }

    }
}