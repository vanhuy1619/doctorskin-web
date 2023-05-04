using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorSkin.Models.Role
{
    public class Roles
    {
        [Key]
        public int stt { set; get; }
        public string email { set; get; }
        public string rolename { set; get; }
    }
}