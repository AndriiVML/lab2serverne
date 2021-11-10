using swp_lab2.DAL.Interfaces;
using swp_lab2.DAL.Models;
using swp_mbvc_test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace swp_mbvc_test.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminBooksController : Controller
    {
        private IUnitOfWork uow;

        public AdminBooksController(IUnitOfWork _ouw)
        {
            uow = _ouw;
        }

        // GET: Admin
        public ActionResult Index()
        {
            var books = uow.Books.GetAll().ToList();
            return View(books);
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = uow.Books.Get(id.Value);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Create()
        {
            var bookToCreate = new BookDTO();

            var sages = uow.Sages.GetAll().ToList();

            foreach (var sage in sages)
            {
                var sagesSelect = new SageSelectDTO
                {
                    Id = sage.Id,
                    Name = sage.Name,
                    IsAdded = false
                };

                bookToCreate.SagesSelect.Add(sagesSelect);
            }

            return View(bookToCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookDTO book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bookToCeate = new Book();
                    var bookSages = new List<Sage>();

                    foreach (var sageSelect in book.SagesSelect)
                    {
                        if(sageSelect.IsAdded == true)
                        {
                            var sage = uow.Sages.Get(sageSelect.Id);

                            if(sage != null)
                            {
                                bookSages.Add(sage);
                            }
                        }
                    }

                    bookToCeate.Name = book.Name;
                    bookToCeate.Description = book.Description;
                    bookToCeate.Sages = bookSages;

                    uow.Books.Create(bookToCeate);
                    uow.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(book);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = uow.Books.Get((long)id);

            if (book == null)
            {
                return HttpNotFound();
            }

            var bookToUpdate = new BookDTO();

            bookToUpdate.Name = book.Name;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Id = book.Id;

            var sages = uow.Sages.GetAll().ToList();

            foreach (var sage in sages)
            {
                var contains = book.Sages.Contains(sage);

                var sagesSelect = new SageSelectDTO
                {
                    Id = sage.Id,
                    Name = sage.Name,
                    IsAdded = contains
                };

                bookToUpdate.SagesSelect.Add(sagesSelect);
            }

            return View(bookToUpdate);
        }

        [HttpPost]
        public ActionResult Edit(BookDTO book)
        {
            var bookToUpdate= new Book();
            var bookSages = new List<Sage>();

            foreach (var sageSelect in book.SagesSelect)
            {
                if (sageSelect.IsAdded == true)
                {
                    var sage = uow.Sages.Get(sageSelect.Id);

                    if (sage != null)
                    {
                        bookSages.Add(sage);
                    }
                }
            }

            bookToUpdate.Id = book.Id;
            bookToUpdate.Name = book.Name;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Sages = bookSages;

            uow.Books.Update(bookToUpdate);
            uow.Save();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var book = uow.Books.Get((long)id);
            if (book != null)
            {
                uow.Books.Delete(book);
                uow.Save();
            }
            return RedirectToAction("Index");
        }
    }
}