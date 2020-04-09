using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CalendarExample.Models
{
    [Table("Customers")]
    public class Customer:Person
    {

        public Customer()
        {
            
            Tasks = new HashSet<SchedulerEvent>();
        }
        public virtual ICollection<SchedulerEvent> Tasks { get; set; }



    }
}