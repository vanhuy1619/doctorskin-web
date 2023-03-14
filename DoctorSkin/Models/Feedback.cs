using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSkin.Models
{
    public class Feedback
    {
        public int sttfb { set; get; }
        public int idbill { set; get; }
        public String cmt { set; get; }
        public Nullable<System.DateTime> datefb { set; get; }
        public bool hidefb { set; get; }
        public String iduser { set; get; }

    }
}