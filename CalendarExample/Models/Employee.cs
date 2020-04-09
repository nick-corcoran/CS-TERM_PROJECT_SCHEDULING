using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CalendarExample.Models
{
    [Table("Employees")]
    public class Employee:Person
    {


        public Employee()
        {

            Tasks = new HashSet<SchedulerEvent>();
        }
        public Guid? EmergencyContactID { get; set; }
       

        public string JobTitle { get; set; }
        public string Groups { get; set; } //multiselect list

        
        public Guid? ManagerID { get; set; }
        public virtual Employee Manager { get; set; }

        public virtual ICollection<SchedulerEvent> Tasks { get; set; }

    }
}