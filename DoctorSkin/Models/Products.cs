using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSkin.Models
{
    public class Products
    {
        [Key]
        public int idp { set; get; }
        public String namep { set; get; }
        public int typep { set; get; }
        public String newprice { set; get; }
        public String oldprice { set; get; }
        public String descr { set; get; }
        public Nullable<System.DateTime> date_up { get; set; }
        public Boolean hide { set; get; }
        public String statep { set; get; }
        public String img { set; get; }
        //public String nameshort { set; get; }
        public int idbrand { set; get; }
        public String metap { set; get; }
        public String avilability { set; get; }

        public String rated { set; get; }
    }
}