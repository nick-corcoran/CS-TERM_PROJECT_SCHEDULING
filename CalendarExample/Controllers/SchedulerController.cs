using CalendarExample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CalendarExample.Controllers
{
    public class SchedulerController : ApiController
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: api/scheduler
        
        public IEnumerable<WebAPIEvent> Get(string id)
        {



            Guid EZEEclap = Guid.Parse(id);
            return db.SchedulerEvents.Where(e => e.clientID == EZEEclap).ToList().Select(e => (WebAPIEvent)e);
            
        }

        // GET: api/scheduler/5
        public WebAPIEvent WebAPIEventGet(int id)
        {
            return (WebAPIEvent)db.SchedulerEvents.Find(id);
        }

        // PUT: api/scheduler/5
        [HttpPut]
        public IHttpActionResult EditSchedulerEvent(int id, WebAPIEvent webAPIEvent)
        {
            var updatedSchedulerEvent = (SchedulerEvent)webAPIEvent;
            Debug.WriteLine(updatedSchedulerEvent.employeeID);
            var e = db.SchedulerEvents.Find(id);
            e.employeeID = db.Employees.Find(updatedSchedulerEvent.employeeID).ID;
            e.StartDate = updatedSchedulerEvent.StartDate;
            e.EndDate = updatedSchedulerEvent.EndDate;
            e.Text = updatedSchedulerEvent.Text;
            db.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // POST: api/scheduler/5
        [HttpPost]
        public IHttpActionResult CreateSchedulerEvent(WebAPIEvent webAPIEvent)
        {
            var newSchedulerEvent = (SchedulerEvent)webAPIEvent;
       
            db.SchedulerEvents.Add(newSchedulerEvent);
            db.SaveChanges();

            return Ok(new
            {
                tid = newSchedulerEvent.Id,
                action = "inserted"
            });
        }

        // DELETE: api/scheduler/5
        [HttpDelete]
        public IHttpActionResult DeleteSchedulerEvent(int id)
        {
            var schedulerEvent = db.SchedulerEvents.Find(id);
            if (schedulerEvent != null)
            {
                db.SchedulerEvents.Remove(schedulerEvent);
                db.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
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
