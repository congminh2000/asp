﻿using SHOPVANGBAC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOPVANGBAC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
                      
            return View();
                  
        }

        public ActionResult Logout()
        {
            Session[CommonConstands.ADMIN_SESSION] = null;
            return Redirect("~/Admin/Login/Login");
        }
    }
}