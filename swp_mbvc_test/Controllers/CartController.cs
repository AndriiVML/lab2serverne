using Microsoft.AspNet.Identity;
using swp_lab2.DAL;
using swp_lab2.DAL.Interfaces;
using swp_lab2.DAL.Models;
using swp_mbvc_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace swp_mbvc_test.Controllers
{
    [Authorize(Roles = "user")]
    public class CartController : Controller
    {
        private IUnitOfWork uow;

        public CartController(IUnitOfWork _ouw)
        {
            uow = _ouw;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var cart = (Cart)Session["cart-" + UserId.ToString()];

            var books = new List<Book>();

            if (cart == null)
            {
                return View(books);
            }
            else
            {
                if (cart.UserId != UserId)
                {
                    return View(books);
                }
            }

            foreach (var id in cart.BookIds)
            {
                var book = uow.Books.Get(id);

                if (book != null)
                {
                    books.Add(book);
                }
            }


            return View(books);
        }


        public ActionResult Delete(long id)
        {
            var UserId = User.Identity.GetUserId();
            var cart = (Cart)Session["cart-" + UserId.ToString()];

            if (cart == null)
            {
                cart = new Cart();

                cart.UserId = UserId;
                cart.BookIds.Remove(id);

                Session["cart-" + UserId.ToString()] = cart;
            }
            else
            {
                cart.BookIds.Remove(id);
                Session["cart-" + UserId.ToString()] = cart;
            }
            return RedirectToAction("Index");
        }

    }
}