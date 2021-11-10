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
    [Authorize]
    public class BooksController : Controller
    {
        private IUnitOfWork uow;

        public BooksController(IUnitOfWork _ouw)
        {
            uow = _ouw;
        }

        // GET: Books
        public ActionResult Index()
        {
            var books = uow.Books.GetAll().ToList();
            var bookIds = new List<long>();

            // filtering
            var UserId = User.Identity.GetUserId();
            var cart = (Cart)Session["cart-" + UserId.ToString()];

            if (cart != null)
            {
                bookIds = cart.BookIds;
            }

            var bookCartDTOs = new List<BookCartDTO>();

            foreach (var book in books)
            {
                var inCart = false;

                foreach (var bookId in bookIds)
                {
                    if (book.Id == bookId)
                    {
                        inCart = true;
                        break;
                    }
                }

                var bookCartDTO = new BookCartDTO()
                {
                    Id = book.Id,
                    Description = book.Description,
                    Name = book.Name,
                    Sages = book.Sages,
                    IsInCart = inCart
                };

                bookCartDTOs.Add(bookCartDTO);
            }


            return View(bookCartDTOs);
        }

        public ActionResult Add(long id)
        {
            var UserId = User.Identity.GetUserId();
            var cart = (Cart)Session["cart-" + UserId.ToString()];

            if (cart ==  null)
            {
                cart = new Cart();

                cart.UserId = UserId;
                cart.BookIds.Add(id);

                Session["cart-" + UserId.ToString()] = cart;
            }
            else
            {
                cart.BookIds.Add(id);
                Session["cart-" + UserId.ToString()] = cart;
            }
            return RedirectToAction("Index");
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

        public ActionResult Create()
        {
            var UserId = User.Identity.GetUserId();
            var UserName = User.Identity.Name;

            var cart = (Cart)Session["cart-" + UserId.ToString()];

            var order = new Order();

            foreach (var bookId in cart.BookIds)
            {
                var book = uow.Books.Get(bookId);

                if(book != null)
                {
                    order.Books.Add(book);
                }
            }

            order.Username = UserName;

            uow.Orders.Create(order);
            uow.Save();

            Session["cart-" + UserId.ToString()] = null;

            return View();
        }

    }
}