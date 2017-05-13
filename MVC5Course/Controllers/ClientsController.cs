using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ClientsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Clients
        public ActionResult BatchUpdate()
        {
            GetClients();

            return View();
        }

        [HttpPost]
        public ActionResult BatchUpdate(ClientsBatchUpdateVM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var client = db.Clients.Find(item.ClientId);
                    client.FirstName = item.FirstName;
                    client.MiddleName = item.MiddleName;
                    client.LastName = item.LastName;
                }
                db.SaveChanges();

                return RedirectToAction("BatchUpdate");
            }

            GetClients();

            return View();
        }

        private void GetClients()
        {
            var clients = db.Clients.Include(c => c.Occupation).Take(10);
            ViewData.Model = clients;
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
