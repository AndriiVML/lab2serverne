using Microsoft.AspNet.Identity;
using swp_lab2.DAL.Interfaces;
using swp_mbvc_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace swp_mbvc_test.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrdersController : Controller
    {
        private IUnitOfWork uow;

        public OrdersController(IUnitOfWork _ouw)
        {
            uow = _ouw;
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = uow.Orders.GetAll().ToList();
            return View(orders);
        }
    }
}