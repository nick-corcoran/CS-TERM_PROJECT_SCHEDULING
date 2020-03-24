﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CalendarExample.Models
{
    public class SchedulerInitializer : CreateDatabaseIfNotExists<SchedulerContext>
    {
        protected override void Seed(SchedulerContext context)
        {
            List<SchedulerEvent> events = new List<SchedulerEvent>()
            {
                new SchedulerEvent()
                {
                    Id = 1,
                    Text = "Event 1",
                    StartDate = new DateTime(2019, 1, 15, 2, 0, 0),
                    EndDate = new DateTime(2019, 1, 15, 4, 0, 0),
                    Completed = "Completed"
                },
                new SchedulerEvent()
                {
                    Id = 2,
                    Text = "Event 2",
                    StartDate = new DateTime(2019, 1, 17, 3, 0, 0),
                    EndDate = new DateTime(2019, 1, 17, 6, 0, 0),
                    Completed = "Completed"
                },
                new SchedulerEvent()
                {
                    Id = 3,
                    Text = "Multiday event",
                    StartDate = new DateTime(2019, 1, 15, 0, 0, 0),
                    EndDate = new DateTime(2019, 1, 20, 0, 0, 0),
                    Completed = "Completed"
                }
            };

            events.ForEach(s => context.SchedulerEvents.Add(s));
            context.SaveChanges();

        }

    }
}