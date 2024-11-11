using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OrderManagementSystem.DAL;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private OrderManagementContext db = new OrderManagementContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            var user = db.Users.FirstOrDefault(u =>
                u.Username == model.Username &&
                u.Password == model.Password);

            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                Session["IsAdmin"] = user.IsAdmin;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}