using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorSkin.Models
{
    public class Users
    {
        public String iduser { set; get; }
        public String name { set; get; }
        public Nullable<System.DateTime> birth { set; get; }
        public String gender { set; get; }
        public String phone { set; get; }
        public String email { set; get; }
        public String password { set; get; }
        public bool hide { set; get; }
        public bool block { set; get; }
        public String ava { set; get; }

    }
}