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
    public class EmployeesController : Controller
    {
        private Company db = new Company();
        



        public ActionResult getIndex()
        {
           
            return View();
        }
        [HttpGet]
        public JsonResult getData(int page, int pagesize=10)
        {
            db.Configuration.ProxyCreationEnabled = false; //tránh tham chiếu tròn
            var model = db.Employees.OrderBy(x=>x.eid).Skip((page-1)*pagesize).Take(pagesize).ToList();
            var list = db.Employees.ToList();
            int total = list.Count;
            if (list !=null)
            {
                return Json(new {data= model,total  = total, status=true}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = model,total = total, status = false }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult UpdateSalary(int? id, int salary)
        {
            
            
            if (id!=null)
            {
                
                db.Employees.Where(x=>x.eid==id).FirstOrDefault().salary= salary;
                var entity = db.Employees.Where(x => x.eid == id).FirstOrDefault();
                db.SaveChanges();
                return Json(new {status = true}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);

            }
        }
        [ChildActionOnly]
       // /////////////---------------------------------------------------------------//
        public PartialViewResult Category()
        {
            var list = db.Departments.ToList();
            return PartialView(list);
        }

        [Route("emps/empbyid/{deptid}")]
        public ActionResult listbyid(int deptid)
        {
            var list = db.Employees.Where(a=>a.deptid ==deptid).ToList();
            return View(list);
        }
        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eid,name,age,addr,salary,image,deptid")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", employee.deptid);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", employee.deptid);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eid,name,age,addr,salary,image,deptid")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", employee.deptid);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
