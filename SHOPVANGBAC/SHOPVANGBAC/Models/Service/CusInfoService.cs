﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SHOPVANGBAC.Domain.DataContext;
using SHOPVANGBAC.Models.ViewModel;
using SHOPVANGBAC.Models.Models;

namespace SHOPVANGBAC.Models.Service
{
    public class CusInfoService
    {
        // Phương thức kiểm tra số lượng sản phẩm
        public static bool CheckNumberProduct(int id,int sl)
        {
            SHOPVANGBACEntities db = new SHOPVANGBACEntities();
            return db.SANPHAMs.Where(s => s.MASP == id).SingleOrDefault().SOLUONG >= sl;
        }
        // Phương thức thêm một đơn hàng
        public static void AddBill(CusInfoViewModel model,int idKhachHang)
        {
            // lấy ra id lớn nhất
            SHOPVANGBACEntities db = new SHOPVANGBACEntities();
            DONHANG don = (from dh in db.DONHANGs
                      orderby dh.MADH descending
                      select dh).Take(1).SingleOrDefault();
            int id = don == null ? 0:don.MADH;
            id += 1;
            // tạo mới đơn hàng
            DONHANG donhang = new DONHANG
            {
                MADH = id,
                MAKH = idKhachHang,
                DIACHIGIAO = model.DiaChiGiao,
                SDT = model.Sdt,
                MOTA = model.MoTa,
                TONGTIEN = model.cart.TotalMoney(),
                TRANGTHAI = "0",
                NGAYDAT = DateTime.Now,
                NGAYGIAO = DateTime.Now.AddDays(7)
            };

                db.DONHANGs.Attach(donhang);
                db.DONHANGs.Add(donhang);
                db.SaveChanges();

           
            // thêm danh sách chi tiết đơn hàng
            AddListDetailBill(model.cart, id);
        }
        // Phương thức thêm chi tiết đơn hàng
        private static void AddListDetailBill(Cart cart,int id)
        {
            SHOPVANGBACEntities db = new SHOPVANGBACEntities();
            foreach (var item in cart.GetList())
            {
                CHITIETDONHANG ct = new CHITIETDONHANG { 
                    MADH = id,
                    MASP = item.Product.MASP,
                    SOLUONG = item.Quantity
                };
                db.CHITIETDONHANGs.Add(ct);
                db.SaveChanges();
            }
            
        }
        // phương thức lấy về mã khách hàng từ mã TK
        public static int GetIdCustomer(long IdAccount)
        {
            SHOPVANGBACEntities db = new SHOPVANGBACEntities();
            return db.KHACHHANGs.Where(s => s.MAKH == IdAccount).SingleOrDefault().MAKH;
        }

    }
}