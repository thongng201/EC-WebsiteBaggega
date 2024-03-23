using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBaggega.Models;

namespace WebsiteBaggega.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        QLTXEntities db = new QLTXEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SanPhamPartial()
        {
            var listSP = db.SANPHAM.OrderBy(sp => sp.TENSP).ToList();
            return View(listSP);
        }
        public ActionResult XemChiTiet(int msp)
        {
            SANPHAM sp = db.SANPHAM.Single(s => s.MASP == msp);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
    }
}