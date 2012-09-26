using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dpuExchange.Models;

namespace dpuExchange.Controllers
{ 
    public class BooksController : Controller
    {
        private BooksDBContext db = new BooksDBContext();

        //
        // GET: /Books/

        public ViewResult Index()
        {
            return View(db.BookItems.ToList());
        }

        //
        // GET: /Books/Details/5

        public ViewResult Details(Guid id)
        {
            Books books = db.BookItems.Find(id);
            return View(books);
        }

        //
        // GET: /Books/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Books/Create

        [HttpPost]
        public ActionResult Create(Books books)
        {
            if (ModelState.IsValid)
            {
                books.BookID = Guid.NewGuid();
                db.BookItems.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(books);
        }
        
        //
        // GET: /Books/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Books books = db.BookItems.Find(id);
            return View(books);
        }

        //
        // POST: /Books/Edit/5

        [HttpPost]
        public ActionResult Edit(Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        //
        // GET: /Books/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Books books = db.BookItems.Find(id);
            return View(books);
        }

        //
        // POST: /Books/Delete/5

        [HttpPost, ActionName("Delete")]
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
    }
}