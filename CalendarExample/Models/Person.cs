using System;
using System.Collections.Generic;

namespace CalendarExample.Models
{
    public class Person
    {
         public Guid ID { get; set; }

        public virtual FullName Name { get; set; }
        public virtual FullAddress HomeAddress { get; set; }

    }
}