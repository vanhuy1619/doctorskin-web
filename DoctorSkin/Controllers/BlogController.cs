﻿using DoctorSkin.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSkin.Controllers
{
    public class BlogController : Controller
    {
        DoctorSkinEntities db = new DoctorSkinEntities();
        // GET: Blog
        public ActionResult Index()
        {
            ViewBag.title = "Kiến thức trị mụn";
            return View();
        }

        public ActionResult getListBlog(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var v = from t in db.BlogDetails where t.hideblog == false select t;
            return PartialView(v.ToList().ToPagedList(pageNumber,pageSize));
        }

        public ActionResult getNewBlog()
        {
            var v = from t in db.BlogDetails where t.hideblog == false orderby t.date_up descending select t;
            return PartialView(v.Take(5).ToList());
        }
    }
}