using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBaggega.Models;

namespace WebsiteBaggega.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: LoaiSanPham
        QLTXEntities db = new QLTXEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoaiSanPhamPartial()
        {
            var listLSP = db.LOAISP.OrderBy(sp => sp.TENLSP).ToList();
            return View(listLSP);
        }
        public ActionResult SanPhamTheoLoai(int maloai)
        {
            var listSP = db.SANPHAM.Where(sp => sp.MALSP == maloai).ToList();
            if (listSP.Count == 0)
            {
                ViewBag.SanPham = "Không có sản phẩm nào thuộc loại này";
            }
            return View(listSP);
        }
    }
}