using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSkin.Models
{
    public class Banners
    {
        [Key]
        public int stt { set; get; }
        public string link { set; get; }
        public string pages { set; get; }
    }
}