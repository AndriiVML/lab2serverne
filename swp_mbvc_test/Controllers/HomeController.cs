using swp_lab2.DAL;
using swp_lab2.DAL.EF;
using swp_lab2.DAL.Interfaces;
using swp_lab2.DAL.Repositories;
using swp_mbvc_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace swp_mbvc_test.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork uow;

        public HomeController(IUnitOfWork _ouw)
        {
            uow = _ouw;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListBooks()
        {
            var books = uow.Books.GetAll().ToList();
            var bookIds = new List<long>();

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
        public ActionResult Add()
        {
            return RedirectToRoute(new { controller = "Account", action = "Login" });
        }


        // GET: Books
        /*      public ActionResult Index()
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

                    if (cart == null)
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

                        if (book != null)
                        {
                            order.Books.Add(book);
                        }
                    }

                    order.Username = UserName;

                    uow.Orders.Create(order);
                    uow.Save();

                    Session["cart-" + UserId.ToString()] = null;

                    return View();
                } */
    }
}     