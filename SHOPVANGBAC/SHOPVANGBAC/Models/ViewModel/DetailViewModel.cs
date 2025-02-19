﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SHOPVANGBAC.Domain.DataContext;

namespace SHOPVANGBAC.Models.ViewModel
{
    public class DetailViewModel
    {
        public ProductViewModel Product { get; set; }
        public IEnumerable<ProductViewModel> ListProductsRelative { get; set; }
        public string Tag { get; set; }
        public IEnumerable<ProductViewModel> ListNewProducts { get; set; }
    }
}