using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookMaintainingSystem.Controllers
{

    //資料的增刪改查
    public class BookController : Controller
    {
        Models.CodeService codeService = new Models.CodeService();
        Models.BookService BookService = new Models.BookService();
        Models.BookEdit BookEdit = new Models.BookEdit();
        Models.Detail BookDetail = new Models.Detail();
        // GET: Book
        public ActionResult Index()//inital
        {

            return View();
        }
        [HttpPost()]
        public JsonResult Search(string BookName, string BookCategoryID, string BookStatus, string BookKeeper)
        {
            Models.BookSearchArg Searcharg = new Models.BookSearchArg
            {
                BookName = BookName,
                BookCategoryID = BookCategoryID,
                BookStatus = BookStatus,
                BookKeeper = BookKeeper
            };

            var result = this.BookService.GetBookByCondtioin(Searcharg);
            return Json(result);
        }
        [HttpPost()]
        public JsonResult GetBookNameData()
        {
            var result = this.codeService.GetBookName();
            return Json(result);
        }
        [HttpPost()]
        public JsonResult GetCategoryData()
        {
            List<SelectListItem> result = this.codeService.GetBookCategory();
            return Json(result);
        }
        [HttpPost()]
        public JsonResult GetKeeperData()
        {
            List<SelectListItem> result = this.codeService.GetBookKeeper();
            return Json(result);
        }
        [HttpPost()]
        public JsonResult GetStatusData()
        {
            List<SelectListItem> result = this.codeService.GetBookStatus();
            return Json(result);
        }
        public ActionResult Edit()
        {
            return View();
        }
        //一開始進入編輯畫面利用id來綁上要編輯的書的資料
        //名稱不能用Edit,會區分不出來
        [HttpPost()]
        public JsonResult EditIndex(int id)
        {
            var result = BookEdit.GetBookByID(id);
            return this.Json(result);
        }
        [HttpPost()]
        public ActionResult Edit(Models.BookEditArg arg)
        {
            if (ModelState.IsValid)
            {
                BookEdit.UpdateBook(arg);
                return this.Json(true);
            }
            return this.Json(false);
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost()]
        public JsonResult Insert(Models.BookEditArg arg)//前端用ajax發要回傳json而不是action
        {
            if (ModelState.IsValid)
            {
                BookService.InsertBook(arg);
                return this.Json(true);
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert(' 新增失敗 ');</script>");
            }
            return this.Json(false);
        }
        public ActionResult Detail()
        {
            return View();
        }
        [HttpPost()]
        public JsonResult GetDetail(string id)
        {
            var result = this.BookDetail.GetBookByID(id);
            return Json(result);
        }

        [HttpPost()]
        public JsonResult Delete(int id)
            
        {
            //if (!codeService.CheckBookExist())
            //{

            //}
            try
            {
                BookEdit.DeleteBookById(id);
                return this.Json(true);
            }
            catch (Exception ex)
            {
                return this.Json(false);
            }
        }
        public ActionResult LendRecord()//inital
        {

            return View();
        }
        [HttpPost()]
        public JsonResult GetLendRecord(int BookID)
        {
            var result = BookEdit.GetRecordByID(BookID);
            return this.Json(result);
        }

    }
}
