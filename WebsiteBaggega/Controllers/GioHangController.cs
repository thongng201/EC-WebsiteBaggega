using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBaggega.Models;

namespace WebsiteBaggega.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        QLTXEntities db = new QLTXEntities();
        public ActionResult Index()
        {
            return View();
        }
        //tạo giỏ hàng
        public List<GioHang> layGioHang()
        {
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang == null)
            {
                lstgiohang = new List<GioHang>();
                Session["GioHang"] = lstgiohang;
            }
            return lstgiohang;
        }
        //thêm giỏ hàng
        public ActionResult ThemGioHang(int msp, string strUrl)
        {
            //Lấy ra giỏ hàng
            List<GioHang> lstGioHang = layGioHang();
            //Kiểm tra sản phẩm có tồn tại hay chưa
            GioHang SP = lstGioHang.Find(s => s.imasp == msp);
            if (SP == null)//nếu chưa có trong giỏ
            {
                SP = new GioHang(msp);
                lstGioHang.Add(SP);
                return Redirect(strUrl);
            }
            else//nếu đã có sản phẩm này trong giỏ
            {
                SP.isL++;
                return Redirect(strUrl);
            }
        }


        //tổng số lượng
        private int tongSL()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
                tsl = lstGioHang.Sum(sp => sp.isL);
            return tsl;
        }


        //tổng thành tiền
        private int tongtien()
        {
            int ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
                ttt += lstGioHang.Sum(sp => sp.iThanhTien);
            return ttt;
        }

        //xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("SanPhamPartial", "SanPham");
            }
            List<GioHang> lstGioHang = layGioHang();
            ViewBag.TongSoLuong = tongSL();
            ViewBag.TongThanhTien = tongtien();

            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult GioHang(FormCollection collection)
        {
            DONHANG ddh = new DONHANG();
            KHACHHANG kh = new KHACHHANG();
            kh.TENKH = collection["tenkh"].ToString();
            kh.SDT = collection["sdt"].ToString();
            kh.DIACHI = collection["diachi"].ToString();
            db.KHACHHANG.Add(kh);
            db.SaveChanges();
            int makh = db.KHACHHANG.Max(sp => sp.MAKH);
            List<GioHang> gh = layGioHang();
            ddh.MAKH = makh;
            ddh.NGAYLAP = DateTime.Now;
            ddh.TONGTIEN = ViewBag.TongThanhTien;
            db.DONHANG.Add(ddh);
            db.SaveChanges();
            foreach (var item in gh)
            {
                CHITIETHD ctdh = new CHITIETHD();
                ctdh.MADH = ddh.MADH;
                ctdh.MASP = item.imasp;
                ctdh.SOLUONG = item.isL;
                db.CHITIETHD.Add(ctdh);
            }
            db.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = tongSL();
            ViewBag.TongThanhTien = tongtien();
            return PartialView();
        }

        public KHACHHANG KH()
        {

            KHACHHANG kh = Session["KHACHHANG"] as KHACHHANG;
            if (kh == null)
            {
                kh = new KHACHHANG();
                //kh.TENKH = f["txttenkh"].ToString();                        
                //kh.SDT = f["txtsdt"].ToString();
                //kh.DIACHI = f["txtdiachi"].ToString();

                Session["GioHang"] = kh;
            }
            return kh;
        }
        public ActionResult themKH(FormCollection f)
        {
            //KHACHHANG kh = KH();
            //if (SP == null)//nếu chưa có trong giỏ
            //{
            //    SP = new GioHang(msp);
            //    lstGioHang.Add(SP);
            //    return Redirect(strUrl);
            //}
            //else//nếu đã có sản phẩm này trong giỏ
            //{
            //    SP.isL++;
            //    return Redirect(strUrl);
            //}
            return View();
        }

        public ActionResult xoaGioHang(int masp)
        {
            //
            List<GioHang> lstGioHang = layGioHang();
            //
            GioHang SP = lstGioHang.Find(s => s.imasp == masp);
            if (SP != null)
            {
                lstGioHang.RemoveAll(s => s.imasp == masp);
                return RedirectToAction("GioHang", "GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult XoaGioHang_All()
        {
            List<GioHang> lstGioHang = layGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult CapNhatGioHang(int masp, FormCollection f)
        {
            List<GioHang> lstGioHang = layGioHang();

            GioHang SP = lstGioHang.Single(s => s.imasp == masp);

            if (SP != null)
                SP.isL = int.Parse(f["txtSL"].ToString());
            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult DatHang()
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "BookStore");
            }
            List<GioHang> lstGiohang = layGioHang();
            ViewBag.TongSoLuong = tongSL();
            ViewBag.TongThanhTien = tongtien();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DONHANG ddh = new DONHANG();
            KHACHHANG kh = new KHACHHANG();
            kh.TENKH = collection["tenkh"].ToString();
            kh.SDT = collection["sdt"].ToString();
            kh.DIACHI = collection["diachi"].ToString();
            db.KHACHHANG.Add(kh);
            db.SaveChanges();
            int masp = db.KHACHHANG.Max(sp => sp.MAKH);
            List<GioHang> gh = layGioHang();
            ddh.MAKH = masp;
            ddh.NGAYLAP = DateTime.Now;
            ddh.TONGTIEN = ViewBag.TongThanhTien;
            db.DONHANG.Add(ddh);
            db.SaveChanges();
            foreach (var item in gh)
            {
                CHITIETHD ctdh = new CHITIETHD();
                ctdh.MADH = ddh.MADH;
                ctdh.MASP = item.imasp;
                ctdh.SOLUONG = item.isL;
                db.CHITIETHD.Add(ctdh);
            }
            db.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}