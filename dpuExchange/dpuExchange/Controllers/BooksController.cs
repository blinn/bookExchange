using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dpuExchange.Models;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace dpuExchange.Controllers
{
    public class BooksController : Controller
    {
        private BooksDBContext db = new BooksDBContext();

        //
        // GET: /Books/


        public ActionResult Index(string param)
        {

            if (param == "0" || param == null)
                return View(db.BookItems.ToList());
            else
            {
                ViewBag.ClassNum = param;
                return View(db.BookItems.ToList());
            }
        }

        [Authorize]
        public ActionResult MyPostings()
        {
            return View(db.BookItems.ToList());
        }

        //
        // GET: /Books/Details/5

        [Authorize]
        public ViewResult Details(Guid id)
        {
            Books books = db.BookItems.Find(id);
            return View(books);
        }

        //
        // GET: /Books/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Books/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(MainModel main)
        {
            Books books = main.BooksModel;
            if (ModelState.IsValid)
            {
                books.BookID = Guid.NewGuid();
                books.UserID = User.Identity.Name;
                books.PostDate = DateTime.Now;
                db.BookItems.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(main);
        }

        //
        // GET: /Books/Edit/5
        [Authorize]
        public ActionResult Edit(Guid id)
        {
            Books books = db.BookItems.Find(id);
            if (books.UserID != User.Identity.Name)
            {
                return RedirectToAction("Index");
            }
            return View(books);
        }

        //
        // POST: /Books/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Books books)
        {
            books.UserID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyPostings");
            }
            return View(books);
        }

        //
        // GET: /Books/Delete/5

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            Books books = db.BookItems.Find(id);
            return View(books);
        }

        //
        // POST: /Books/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Books books = db.BookItems.Find(id);
            db.BookItems.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public string searchForISBN(String titleSearch)
        {
            HttpWebRequest request = WebRequest.Create("http://isbndb.com/api/books.xml?access_key=2PFWPRKF&index1=title&value1=" + titleSearch) as HttpWebRequest;
            string result = null;
            using (HttpWebResponse resp = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}