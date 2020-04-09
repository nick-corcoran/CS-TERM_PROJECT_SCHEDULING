using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CalendarExample.Models
{
    public class Task
    {
        public Guid ID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        
        public int ActualDuration { get; set; }



        public Guid clientID { get; set; }
        public Guid employeeID { get; set; }

        [ForeignKey("clientID")]
        public virtual Person client { get; set; }

        [ForeignKey("employeeID")]
        public virtual Person employee { get; set; }


        
    }


}
