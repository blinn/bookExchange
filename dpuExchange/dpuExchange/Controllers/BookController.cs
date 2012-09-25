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
    public class BookController : Controller
    {
        private BookDBContext db = new BookDBContext();

        //
        // GET: /Book/

        public ViewResult Index()
        {
            return View(db.Book.ToList());
        }

        //
        // GET: /Book/Details/5

        public ViewResult Details(int id)
        {
            Books books = db.Book.Find(id);
            return View(books);
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(Books books)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(books);
        }
        
        //
        // GET: /Book/Edit/5
 
        public ActionResult Edit(int id)
        {
            Books books = db.Book.Find(id);
            return View(books);
        }

        //
        // POST: /Book/Edit/5

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
        // GET: /Book/Delete/5
 
        public ActionResult Delete(int id)
        {
            Books books = db.Book.Find(id);
            return View(books);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Books books = db.Book.Find(id);
            db.Book.Remove(books);
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