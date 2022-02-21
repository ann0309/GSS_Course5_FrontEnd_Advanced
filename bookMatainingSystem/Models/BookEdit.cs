using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookMaintainingSystem.Models
{
    public class BookEdit : Controller
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
        public Models.BookEditArg GetBookByID(int id)
        {
            DataTable dt = new DataTable();
            string sql = @"	SELECT
                                bd.BOOK_ID,
	                            bd.BOOK_NAME,
	                            bd.BOOK_AUTHOR,
	                            bd.BOOK_PUBLISHER,
	                            bd.BOOK_NOTE,
	                            bd.BOOK_BOUGHT_DATE AS BOOK_BOUGHT_DATE,
	                            bc.BOOK_CLASS_ID,
                                bc.BOOK_CLASS_NAME,
                                bd.BOOK_STATUS ,	                            
	                            bd.BOOK_KEEPER
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
                cmd.Parameters.Add(new SqlParameter("@BookID", id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();

            }
            return this.MapBookDataToModel(dt);
        }
        public List<Models.LendRecord> GetRecordByID(int id)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT blr.LEND_DATE AS LEND_DATE,
                                  blr.KEEPER_ID AS KEEPER_ID,
                                  m.USER_ENAME AS USER_ENAME,
                                  m.USER_CNAME AS USER_CNAME
                                  FROM BOOK_LEND_RECORD AS blr 
                                  LEFT JOIN MEMBER_M AS m 
                                  ON blr.KEEPER_ID=m.USER_ID 
                                  WHERE BOOK_ID=@BookID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();

            }
            return this.MapRecordDataToModel(dt);
        }
        private List<Models.LendRecord> MapRecordDataToModel(DataTable bookData)
        {
            List<Models.LendRecord> result = new List<LendRecord>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new LendRecord()
                {
                    KeeperId = row["KEEPER_ID"].ToString(),
                    LendDate = (DateTime)row["LEND_DATE"],
                    UserEname = row["USER_ENAME"].ToString(),
                    UserCname = row["USER_CNAME"].ToString(),
                });
            }
            return result;

        }
        private Models.BookEditArg MapBookDataToModel(DataTable bookData)
        {
            Models.BookEditArg result = new BookEditArg();//???
            foreach (DataRow row in bookData.Rows)
            {
                result = new BookEditArg()
                {
                    BookID = (int)row["BOOK_ID"],
                    BookName = row["BOOK_NAME"].ToString(),
                    BookAuthor = row["BOOK_AUTHOR"].ToString(),
                    BookPublisher = row["BOOK_PUBLISHER"].ToString(),
                    BookBoughtDate = (DateTime)row["BOOK_BOUGHT_DATE"],
                    BookCategoryID = row["BOOK_CLASS_ID"].ToString(),
                    BookCategoryName = row["BOOK_CLASS_NAME"].ToString(),
                    BookStatus = row["BOOK_STATUS"].ToString(),
                    BookKeeper = row["BOOK_KEEPER"].ToString()

                };
            }

            return result;

        }

        public void DeleteBookById(int id)
        {
            try
            {
                string sql = "Delete FROM BOOK_DATA Where BOOK_ID=@BookID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BookID", id));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateBook(Models.BookEditArg arg)
        {
            try
            {
                string sql = @"  UPDATE BOOK_DATA
                                 SET BOOK_NAME = @BookName
	                                ,BOOK_AUTHOR = @BookAuthor
	                                ,BOOK_PUBLISHER = @BookPublisher
	                                ,BOOK_NOTE = @BookContent
	                                ,BOOK_BOUGHT_DATE = @BookBoughtDate
	                                ,BOOK_CLASS_ID = @BookCategoryID
	                                ,BOOK_STATUS = @BookStatus
	                                ,MODIFY_DATE = GETDATE()
                                    ,MODIFY_USER='180'
	                                ,BOOK_KEEPER =
	                                 CASE
		                                 WHEN BOOK_STATUS = 'A' OR
			                                 BOOK_STATUS = 'D' THEN ''
		                                 ELSE @BookKeeper
	                                 END
                                  WHERE BOOK_ID = @BookID
                                  IF @BookStatus = 'B' OR @BookStatus = 'C'
                                  INSERT INTO BOOK_LEND_RECORD (BOOK_ID, KEEPER_ID, LEND_DATE,CRE_DATE,MOD_DATE)
		                VALUES (@BookID, @BookKeeper, GETDATE(),GETDATE(),GETDATE())

                                ";
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
                    cmd.Parameters.Add(new SqlParameter("@BookStatus", arg.BookStatus));
                    cmd.Parameters.Add(new SqlParameter("@BookKeeper", arg.BookKeeper == null ? string.Empty : arg.BookKeeper));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
