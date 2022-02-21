using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookMaintainingSystem.Models
{
    public class BookService : Controller
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


        //利用bookserch的東西來做搜尋
        public List<Models.Book> GetBookByCondtioin(Models.BookSearchArg arg)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT bd.BOOK_ID,
	                                bc.BOOK_CLASS_ID,
                                    bc.BOOK_CLASS_NAME
                                   ,bd.BOOK_NAME
                                   ,bd.BOOK_BOUGHT_DATE
                                   ,bc1.CODE_NAME
                                   ,M.USER_ENAME
                                FROM BOOK_DATA bd
                                LEFT JOIN BOOK_CLASS bc
	                                ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
                                LEFT JOIN BOOK_CODE bc1
	                                ON bd.BOOK_STATUS = bc1.CODE_ID AND bc1.CODE_TYPE_DESC = '書籍狀態'
                                LEFT JOIN MEMBER_M M
	                                ON bd.BOOK_KEEPER = M.USER_ID
                                WHERE (bc.BOOK_CLASS_ID=@BookCategoryID or @BookCategoryID='') AND
                                      (M.USER_ID=@BookKeeper or @BookKeeper='') AND
                                      (UPPER(bd.BOOK_NAME) LIKE UPPER('%' + @BookName + '%')or @BookName='') AND
                                      (bd.BOOK_STATUS=@BookStatus or @BookStatus='')
                                ORDER BY bd.BOOK_BOUGHT_DATE DESC";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                //利用parameter做select
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));//第一個參數:user輸入,第二個參數:要加的值(使用者使用的text)
                cmd.Parameters.Add(new SqlParameter("@BookCategoryID", arg.BookCategoryID == null ? string.Empty : arg.BookCategoryID));
                cmd.Parameters.Add(new SqlParameter("@BookKeeper", arg.BookKeeper == null ? string.Empty : arg.BookKeeper));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", arg.BookStatus == null ? string.Empty : arg.BookStatus));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }


        private List<Models.Book> MapBookDataToList(DataTable bookData)
        {
            List<Models.Book> result = new List<Book>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new Book()//Book.cs
                {
                    BookID = row["BOOK_ID"].ToString(),
                    BookName = row["BOOK_NAME"].ToString(),
                    BookCategoryName = row["BOOK_CLASS_NAME"].ToString(),
                    BookBoughtDate = (DateTime)row["BOOK_BOUGHT_DATE"],
                    BookStatus = row["CODE_NAME"].ToString(),
                    BookKeeper = row["USER_ENAME"].ToString()


                });
            }
            return result;
        }

        public ActionResult InsertBook(Models.BookEditArg arg)
        {
            string sql = @" INSERT INTO BOOK_DATA (
                            BOOK_NAME, 
                            BOOK_AUTHOR, 
                            BOOK_PUBLISHER, 
                            BOOK_NOTE, 
                            BOOK_BOUGHT_DATE, 
                            BOOK_CLASS_ID,
                            BOOK_STATUS,
                            BOOK_KEEPER,
                            CREATE_DATE,
                            CREATE_USER)                           
	                        VALUES (@BookName, 
                                    @BookAuthor, 
                                    @BookPublisher, 
                                    @BookContent, 
                                    @BookBoughtDate, 
                                    @BookCategoryID,
                                    'A',
                                    '',
                                    GETDATE(),
                                    '180') 
                                    Select SCOPE_IDENTITY()";
            int id;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", arg.BookID));
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", arg.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BookPublisher", arg.BookPublisher));
                cmd.Parameters.Add(new SqlParameter("@BookContent", arg.BookContent == null ? string.Empty : arg.BookContent));
                cmd.Parameters.Add(new SqlParameter("@BookCategoryID", arg.BookCategoryID));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", arg.BookBoughtDate));
                id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return View();
        }
        //public ActionResult CheckBookExist(int id)
        //{
        //    DataTable dt = new DataTable();
        //    string sql = @"	SELECT
        //                        bd.BOOK_ID,
	       //                     bd.BOOK_NAME,
	       //                     bd.BOOK_AUTHOR,
	       //                     bd.BOOK_PUBLISHER,
	       //                     bd.BOOK_NOTE,
	       //                     bd.BOOK_BOUGHT_DATE AS BOOK_BOUGHT_DATE,
	       //                     bc.BOOK_CLASS_ID,
        //                        bc.BOOK_CLASS_NAME,
        //                        bd.BOOK_STATUS ,	                            
	       //                     bd.BOOK_KEEPER
        //                    FROM BOOK_DATA AS bd
        //                    LEFT JOIN BOOK_CLASS AS bc
	       //                     ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
        //                    LEFT JOIN BOOK_CODE AS bc1
	       //                     ON bd.BOOK_STATUS = bc1.CODE_ID
		      //                      AND bc1.CODE_TYPE_DESC = '書籍狀態'
        //                    LEFT JOIN MEMBER_M AS mm
	       //                     ON bd.BOOK_KEEPER = mm.USER_ID
        //                    WHERE BOOK_ID=@BookID";

        //    using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.Add(new SqlParameter("@BookID", id));
        //        SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
        //        sqlAdapter.Fill(dt);
        //        conn.Close();

        //    }
        //    return this.MapBookExist(dt);
        //}
        //private List<SelectListItem> MapBookExist(DataTable dt)
        //{
        //    List<SelectListItem> result = new List<SelectListItem>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        result.Add(new SelectListItem()
        //        {
        //            Text = row["CODE_NAME"].ToString(),
        //            Value = row["CODE_ID"].ToString()
        //        });
        //    }
        //    return result;
        //}
    }
}


