using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalendarExample.Models;

namespace CalendarExample.Controllers
{
    public class EmployeesController : Controller
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: Employees
        public ActionResult Index()
        {
            var people = db.Employees.Include(e => e.HomeAddress).Include(e => e.Name);
            return View(people.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(Guid? id)
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
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "Street");
            ViewBag.ID = new SelectList(db.FullNames, "ID", "FirstName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Employee employee, FullAddress fullAddress, FullName fullName)
        {
            if (ModelState.IsValid)
            {
                employee.ID = Guid.NewGuid();
                fullName.ID = employee.ID;
                fullAddress.ID = employee.ID;
                employee.Name = fullName;
                employee.HomeAddress = fullAddress;
         
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "Street", employee.ID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "FirstName", employee.ID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(Guid? id)
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
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "Street", employee.ID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "FirstName", employee.ID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee, FullAddress fullAddress, FullName fullName, string JobTitle, string Groups)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SchedulerContext())
                {
                    var result = db.Employees.SingleOrDefault(b => b.ID == employee.ID);
                    var result2 = db.FullNames.SingleOrDefault(b => b.ID == fullName.ID);
                    var result3 = db.FullAddresses.SingleOrDefault(b => b.ID == fullAddress.ID);

                    if (result != null & result2 != null & result3 != null)
                    {
                        Debug.WriteLine("-----------------------------------------------------------------------------------Made It" + employee.ID);
                        result2 = fullName;
                        result3 = fullAddress;
                        result.Name = result2;
                        result.HomeAddress = result3;
                        result.JobTitle = JobTitle;
                        result.Groups = Groups;

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(Guid? id)
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
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);

            db.FullAddresses.Remove(employee.HomeAddress);
            db.FullNames.Remove(employee.Name);
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
