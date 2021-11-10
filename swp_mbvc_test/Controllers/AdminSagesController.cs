using swp_lab2.DAL.Interfaces;
using swp_lab2.DAL.Models;
using swp_mbvc_test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace swp_mbvc_test.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminSagesController : Controller
    {
        private IUnitOfWork uow;

        public AdminSagesController(IUnitOfWork _ouw)
        {
            uow = _ouw;
        }

        // GET: AdminSages
        public ActionResult Index()
        {
            var sages = uow.Sages.GetAll().ToList();
            return View(sages);
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sage = uow.Sages.Get(id.Value);

            if (sage == null)
            {
                return HttpNotFound();
            }
            return View(sage);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SageDTO sage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] uploadedFile = new byte[sage.Photo.InputStream.Length];
                    sage.Photo.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                    var sageToCreate = new Sage()
                    {
                        Age = sage.Age,
                        City = sage.City,
                        Name = sage.Name,
                        Photo = uploadedFile
                    };

                    uow.Sages.Create(sageToCreate);
                    uow.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(sage);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sage = uow.Sages.Get((long)id);

            if (sage == null)
            {
                return HttpNotFound();
            }

            return View(sage);
        }

        [HttpPost]
        public ActionResult Edit(Sage sage)
        {
            uow.Sages.Update(sage);
            uow.Save();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var sage = uow.Sages.Get((long)id);
            if (sage != null)
            {
                uow.Sages.Delete(sage);
                uow.Save();
            }
            return RedirectToAction("Index");
        }
    }
}