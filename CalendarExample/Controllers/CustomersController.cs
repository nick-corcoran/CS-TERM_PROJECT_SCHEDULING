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
    public class CustomersController : Controller
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.HomeAddress).Include(c => c.Name);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "Street");
            ViewBag.ID = new SelectList(db.FullNames, "ID", "FirstName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] Customer customer, string FirstName, string LastName, string MiddleName, string Country, string Province, string City , string Street, string PostalCode)
        {
            if (ModelState.IsValid)
            {
                FullName full = new FullName();
                FullAddress fullA = new FullAddress();
                Guid fullGUID = Guid.NewGuid();
                Guid fullAGuid = Guid.NewGuid();
                Debug.WriteLine("------------------------------------------------------------------------" + Country);
                full.FirstName = FirstName;
                full.LastName = LastName;
                full.ID = fullGUID;


                fullA.Country = Country;
                fullA.City = City;
                fullA.Street = Street;
                fullA.Province = Province;
                fullA.ID = fullAGuid;
                fullA.PostalCode = PostalCode;


                customer.ID = Guid.NewGuid();

                customer.Name = full;
                customer.HomeAddress = fullA;

                full.person = customer;
                fullA.person = customer;

                db.FullNames.Add(full);
                db.FullAddresses.Add(fullA);
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "Street", customer.ID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "FirstName", customer.ID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer,FullAddress fullAddress, FullName fullName)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SchedulerContext())
                {
                    var result = db.Customers.SingleOrDefault(b => b.ID == customer.ID);
                    var result2 = db.FullNames.SingleOrDefault(b => b.ID == fullName.ID);
                    var result3 = db.FullAddresses.SingleOrDefault(b => b.ID == fullAddress.ID);
                    var query = db.Customers.Count();

                    if (result != null & result2!=null & result3 !=null)
                    {
                        Debug.WriteLine("-----------------------------------------------------------------------------------Made It");
                        result2= fullName;
                        result3= fullAddress;
                        result.Name = result2;
                        result.HomeAddress = result3;

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
            
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            db.FullAddresses.Remove(customer.HomeAddress);
            db.FullNames.Remove(customer.Name);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        /*// GET: Customers/Tasks/index
        public ActionResult Tasks(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }*/














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
