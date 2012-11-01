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

        public ActionResult searchForISBN(MainModel model)
        {
            Searches search = new Searches();
            search = model.SearchModel;
            HttpWebRequest request = WebRequest.Create("http://isbndb.com/api/books.xml?access_key=2PFWPRKF&index1=title&value1=" + search.SearchByTitle) as HttpWebRequest;
            string result = null;
            using (HttpWebResponse resp = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            IsbnResults bookResults = new IsbnResults();
            StringBuilder output = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(new StringReader(result)))
            {
                reader.ReadToFollowing("BookList");
                reader.MoveToFirstAttribute();
                reader.MoveToNextAttribute();
                reader.MoveToNextAttribute();
                reader.MoveToNextAttribute();
                string shown_results = reader.Value;
                int numResults;
                bool parsed = Int32.TryParse(shown_results, out numResults);
                if (numResults > 4)
                {
                    numResults = 4;
                }
                model.IsbnCollection = new IsbnResultList();
                for (int i = 0; i < numResults; i++)
                {

                    reader.ReadToFollowing("BookData");
                    reader.MoveToFirstAttribute();
                    reader.MoveToNextAttribute();
                    reader.MoveToNextAttribute();
                    string isbn = reader.Value;
                    reader.ReadToFollowing("Title");
                    string title = reader.ReadString();
                    reader.ReadToFollowing("AuthorsText");
                    string author = reader.ReadString();



                    bookResults.Isbn = isbn;
                    if (i == 0)
                    {
                        model.IsbnCollection.result1.Isbn = isbn;
                        model.IsbnCollection.result1.Title = title;
                        model.IsbnCollection.result1.Author = author;
                        model.IsbnCollection.numRecords = 1;
                    }
                    else if (i == 1)
                    {
                        model.IsbnCollection.result2.Isbn = isbn;
                        model.IsbnCollection.result2.Title = title;
                        model.IsbnCollection.result2.Author = author;
                        model.IsbnCollection.numRecords = 2;
                    }
                    else if (i == 2)
                    {
                        model.IsbnCollection.result3.Isbn = isbn;
                        model.IsbnCollection.result3.Title = title;
                        model.IsbnCollection.result3.Author = author;
                        model.IsbnCollection.numRecords = 3;
                    }
                    else
                    {
                        model.IsbnCollection.result4.Isbn = isbn;
                        model.IsbnCollection.result4.Title = title;
                        model.IsbnCollection.result4.Author = author;
                        model.IsbnCollection.numRecords = 4;
                    }

                }
            }
            return View("Create", model);
        }
    }
}