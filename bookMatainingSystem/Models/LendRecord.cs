using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace bookMaintainingSystem.Models
{
    public class LendRecord
    {
        public int BookID { get; set; }

        [DisplayName("借書日期")]
        [DataType(DataType.Date)]
        public DateTime LendDate { get; set; }

        [DisplayName("借閱人ID")]
        public string KeeperId { get; set; }
        [DisplayName("借閱人中文姓名")]
        public string UserCname { get; set; }
        [DisplayName("借閱人英文姓名")]
        public string UserEname { get; set; }
    }
}