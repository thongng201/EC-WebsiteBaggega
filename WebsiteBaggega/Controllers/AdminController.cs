 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBaggega.Models;

namespace WebsiteBaggega.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        QLTXEntities db = new QLTXEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult laydsSP()
        {
            var sp = from s in db.SANPHAM select s;
            return View(sp);
        }
        [HttpGet]
        public SANPHAM laySP(int msp)
        {
            return db.SANPHAM.FirstOrDefault(x => x.MASP == msp);
        }
        [HttpPost]
        public ActionResult Details(int msp)
        {
            var dtpd = db.SANPHAM.Where(s => s.MASP == msp).First();
            return View(dtpd);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, SANPHAM sp)
        {
            var s = collection["TENSP"];

            sp.TENSP = s;
            db.SANPHAM.Add(sp);
            db.SaveChanges();
            return RedirectToAction("laydsSP");
        }

        public ActionResult Edit(int msp)
        {
            var sp = db.SANPHAM.First(m => m.MASP == msp);
            return View(sp);
        }

        [HttpPost]
        public ActionResult Edit(int msp, FormCollection collection)
        {

            var bh = db.SANPHAM.First(m => m.MASP == msp);
            var tensp = collection["TENSP"];
            bh.MASP = msp;
            bh.TENSP = tensp;

            UpdateModel(bh);
            db.SaveChanges();
            return RedirectToAction("laydsSP");

        }

        public ActionResult Delete(int msp)
        {
            var sp = db.SANPHAM.First(m => m.MASP == msp);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Delete(int msp, FormCollection collection)
        {
            var sp = db.SANPHAM.Where(m => m.MASP == msp).First();
            db.SANPHAM.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("laydsSP");
        }
    }
}