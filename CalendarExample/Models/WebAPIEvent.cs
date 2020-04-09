﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarExample.Models
{
    public class WebAPIEvent
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string completed { get; set; }

        public Guid clientid{ get; set; }
        public Guid employeeid { get; set; }

        public static explicit operator WebAPIEvent(SchedulerEvent schedulerEvent)
        {
            return new WebAPIEvent
            {
                id = schedulerEvent.Id,
                text = schedulerEvent.Text,
                start_date = schedulerEvent.StartDate.ToString("yyyy-MM-dd HH:mm"),
                end_date = schedulerEvent.EndDate.ToString("yyyy-MM-dd HH:mm"),
                completed = schedulerEvent.Completed,
                clientid = schedulerEvent.clientID,
                employeeid = schedulerEvent.clientID
            };
        }

        public static explicit operator SchedulerEvent(WebAPIEvent schedulerEvent)
        {
            return new SchedulerEvent
            {
                Id = schedulerEvent.id,
                Text = schedulerEvent.text,
                StartDate = DateTime.Parse(
                    schedulerEvent.start_date,
                    System.Globalization.CultureInfo.InvariantCulture),
                EndDate = DateTime.Parse(
                    schedulerEvent.end_date,
                    System.Globalization.CultureInfo.InvariantCulture),
                Completed = schedulerEvent.completed,
                clientID = schedulerEvent.clientid,
                employeeID = schedulerEvent.employeeid
            };
        }

    }
}