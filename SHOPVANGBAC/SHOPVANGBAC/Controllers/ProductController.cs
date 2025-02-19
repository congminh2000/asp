﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPVANGBAC.Models.ViewModel;
using SHOPVANGBAC.Models.Service;

namespace SHOPVANGBAC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
       
        public ActionResult Index(string cateKey, int page = 1)
        {
            int pageSize = 8;
            ViewData["pageSize"] = pageSize;
            ProductCategoryViewModel prctViewModel = new ProductCategoryViewModel();

            //lấy ra danh sách sản phẩm theo loại
            if(cateKey=="tat-ca")
            {
                prctViewModel.ListProductCategory = ProductCategoryService.LoadProductAll();
            }
            else
            {
                if(cateKey == "bac")
                {
                    prctViewModel.ListProductCategory = ProductCategoryService.LoadProductBac();
                }
                else
                {
                    prctViewModel.ListProductCategory = ProductCategoryService.LoadProductVang();
                }
            }

            int totalRecord = prctViewModel.ListProductCategory.Count;
            prctViewModel.CateKey = cateKey;
            prctViewModel.Index = page;
            prctViewModel.TotalPage= (int)(Math.Ceiling(((double)totalRecord / pageSize)));

            return View(prctViewModel);
        }

        public JsonResult ListName(String q)
        {
            var data = ProductService.ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1)
        {
            int pageSize = 8;
            ViewData["pageSize"] = pageSize;
            ViewBag.Keyword = keyword;
            ProductCategoryViewModel prctViewModel = new ProductCategoryViewModel();

            //lấy ra danh sách sản phẩm theo tên
            prctViewModel.ListProductCategory = ProductService.Search(keyword);

            int totalRecord = prctViewModel.ListProductCategory.Count;
            ViewBag.TotalRecord = totalRecord;
            prctViewModel.Index = page;
            prctViewModel.TotalPage = (int)(Math.Ceiling(((double)totalRecord / pageSize)));

            return View(prctViewModel);
        }
    }
}