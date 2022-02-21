using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace bookMaintainingSystem.Models
{
    public class BookSearchArg
    {
        //放變數
        public string BookID { get; set; }

        [DisplayName("圖書類別編號")]
        public string BookCategoryID { get; set; }
        [DisplayName("圖書類別名稱")]
        public string BookCategoryName { get; set; }

        [DisplayName("借閱人")]
        public string BookKeeper { get; set; }

        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }

        [DisplayName("書名")]
        public string BookName { get; set; }

        
        [DisplayName("購書日期")]
        [DataType(DataType.Date)]
        public DateTime BookBoughtDate { get; set; }
    }
}