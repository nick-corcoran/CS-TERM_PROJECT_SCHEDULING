using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CalendarExample.Models
{
    public class SchedulerEvent
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Completed { get; set; }




        public Guid clientID { get; set; }
        public Guid employeeID { get; set; }

        [ForeignKey("clientID")]
        public virtual Person client { get; set; }

        [ForeignKey("employeeID")]
        public virtual Person employee { get; set; }
    }
}