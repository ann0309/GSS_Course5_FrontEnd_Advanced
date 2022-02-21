using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace bookMaintainingSystem.Models
{
    public class BookEditArg
    {
        [DisplayName("書本ID")]
        public int BookID { get; set; }

        [DisplayName("書名")]
        [Required(ErrorMessage = "書名必填")]
        public string BookName { get; set; }

        [DisplayName("圖書類別")]
        [Required(ErrorMessage = "類別必填")]
        public string BookCategoryID { get; set; }


        [DisplayName("圖書類別名稱")]

        public string BookCategoryName { get; set; }
        [DisplayName("借閱狀態")]
        //[Required(ErrorMessage = "狀態必填")]
        public string BookStatus { get; set; }

        [DisplayName("借閱人")]
        public string BookKeeper { get; set; }

        [DisplayName("作者")]
        [Required(ErrorMessage = "作者必填")]
        public string BookAuthor { get; set; }

        [DisplayName("出版商")]
        [Required(ErrorMessage = "出版商必填")]
        public string BookPublisher { get; set; }
        [DisplayName("內容簡介")]
        public string BookContent { get; set; }


        [DisplayName("購書日期")]
        [Required(ErrorMessage = "購書日期必填")]
        [DataType(DataType.Date)]
        public DateTime BookBoughtDate { get; set; }
    }
}