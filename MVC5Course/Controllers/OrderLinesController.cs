﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class OrderLinesController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: OrderLines
        public ActionResult Index(int id)
        {
            //var orderLine = db.OrderLine.Include(o => o.Order).Include(o => o.Product);
            var orderLine = db.OrderLine
                 .Where(p => p.ProductId == id)
                 .Include(o => o.Order).Include(o => o.Product)
                 .Take(5).ToList();
            return View(orderLine);
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