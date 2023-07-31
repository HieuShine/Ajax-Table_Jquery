using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demoCuoiky_2.Models;

namespace demoCuoiky_2.Controllers
{
    public class AccoutsController : Controller
    {
        private Company db = new Company();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Accouts.Where(a=>a.username== username && a.password == password).FirstOrDefault();
            if(user == null)
            {
                ViewBag.errMsg = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View("Login");
            }    
            else
            {
                Session["username"] = username;
                return RedirectToAction("Index", "Employees");
            }
        
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index","Employees"); }
        // GET: Accouts
        public ActionResult Index()
        {
            return View(db.Accouts.ToList());
        }

        // GET: Accouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accout accout = db.Accouts.Find(id);
            if (accout == null)
            {
                return HttpNotFound();
            }
            return View(accout);
        }

        // GET: Accouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,role")] Accout accout)
        {
            if (ModelState.IsValid)
            {
                db.Accouts.Add(accout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accout);
        }

        // GET: Accouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accout accout = db.Accouts.Find(id);
            if (accout == null)
            {
                return HttpNotFound();
            }
            return View(accout);
        }

        // POST: Accouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,role")] Accout accout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accout);
        }

        // GET: Accouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accout accout = db.Accouts.Find(id);
            if (accout == null)
            {
                return HttpNotFound();
            }
            return View(accout);
        }

        // POST: Accouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accout accout = db.Accouts.Find(id);
            db.Accouts.Remove(accout);
            db.SaveChanges();
            return RedirectToAction("Index");
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
