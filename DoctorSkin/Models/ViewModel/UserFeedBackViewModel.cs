using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSkin.Models.ViewModel
{
    public class UserFeedBackViewModel
    {
        [Key]
        public int sttfb { set; get; }
        public String cmt { set; get; }
        public Nullable<System.DateTime> datefb { set; get; }
        public String nameu { set; get; }
        public String avau { set; get; }
    }
}