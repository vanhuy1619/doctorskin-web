﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSkin.Models
{
    public class Bills
    {
        [Key]
        public int sttbill { set; get; }
        public int idp { set; get; }
        public int quantity { set; get; }
        public String totalmoney { set; get; }
        public String idbill { set; get; }
        public String iduser { set; get; }
        public string note { set; get; }
        public string status { set; get; }

    }
}