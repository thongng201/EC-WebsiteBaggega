using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBaggega.Models;

namespace WebsiteBaggega.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        QLTXEntities db = new QLTXEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string pass)
        {
            if (username == null || pass == null)
                return RedirectToAction("Login", "TaiKhoan");
            TaiKhoan taikhoan = db.TaiKhoan.Single(tk => tk.username == username && tk.password == pass);
            if (taikhoan != null)
                return RedirectToAction("laydsSP", "Admin");
            return RedirectToAction("Login", "TaiKhoan");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: TaiKhoan/Register
        [HttpPost]
        public ActionResult Register(string username, string pass, string address)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(address))
            {
                ViewBag.ErrorMessage = "Vui lòng điền đầy đủ thông tin.";
                return View();
            }

            // Kiểm tra xem username đã tồn tại trong CSDL chưa
            if (db.TaiKhoan.Any(tk => tk.username == username))
            {
                ViewBag.ErrorMessage = "Tài khoản đã tồn tại. Vui lòng chọn tên đăng nhập khác.";
                return View();
            }

            // Tạo một đối tượng TaiKhoan mới và lưu thông tin vào CSDL
            TaiKhoan newTaiKhoan = new TaiKhoan()
            {
                username = username,
                password = pass,
                address = address
            };

            db.TaiKhoan.Add(newTaiKhoan);

            db.SaveChanges();

            // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
            return RedirectToAction("Login", "TaiKhoan");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("SanPhamPartial", "SanPham");
        }

    }
}