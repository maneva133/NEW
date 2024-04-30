using NEW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEW.Controllers
{
    public class EventController : Controller
    {
        public static List<EventModel> events = new List<EventModel>()
        {
            new EventModel(){Id=1, Name="Grozdober",Location="Kavadarci"},
            new EventModel(){Id=2,Name="PivoF",Location="Prilep"},
            new EventModel(){Id=3,Name="ZSlavejce",Location="Skopje"}
        };

        public ActionResult tableEvents()
        {
            return View(events);
        }

        public ActionResult addEvents()
        {
            EventModel model = new EventModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult addEvents(EventModel model)
        {
            if (ModelState.IsValid)
            {
                int newId = events.Count() + 1;
                model.Id = newId;
                events.Add(model);
                return RedirectToAction("eventDetails", new { i = newId });
            }
            return View(model);
        }

        public ActionResult eventDetails(int i)
        {
            var em = events[i - 1];
            return View(em);
        }

        [HttpPost]
        public ActionResult eventDetails(EventModel model)
        {
            return RedirectToAction("tableEvents");

        }

        public ActionResult updateEvent(int i)
        {
            EventModel model = new EventModel();
            model = events.FirstOrDefault(e => e.Id == i);
            return View(model);
        }
        [HttpPost]
        public ActionResult updateEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var existingEvent = events.FirstOrDefault(e => e.Id == model.Id);
                if (existingEvent != null)
                {
                    existingEvent.Name = model.Name;
                    existingEvent.Location = model.Location;

                    return RedirectToAction("tableEvents");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult deleteEvent(int i) 
        {
            var en = events.FirstOrDefault(e => e.Id == i);
            events.Remove(en);
            return RedirectToAction("tableEvents");
        }
    }
}