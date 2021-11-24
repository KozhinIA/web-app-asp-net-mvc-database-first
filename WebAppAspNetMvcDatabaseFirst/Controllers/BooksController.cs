using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebAppAspNetMvcDatabaseFirst.Models;
using WebAppAspNetMvcDatabaseFirst.Models.Entitys;

namespace WebAppAspNetMvcDatabaseFirst.Controllers
{
    public class BooksController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var books = MappingBooks(db.Books.ToList());

            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var book = new BookViewModel();
            return View(book);
        }

        [HttpPost]
        public ActionResult Create(BookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var book = new Book();
            book.CreateAt = DateTime.Now;
            MappingBook(model, book, db);

            db.Books.Add(book);
            db.SaveChanges();

            return RedirectPermanent("/Books/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return RedirectPermanent("/Books/Index");

            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectPermanent("/Books/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var book = MappingBooks(db.Books.Where(x=> x.Id == id).ToList()).FirstOrDefault(x => x.Id == id);
            if (book == null)
                return RedirectPermanent("/Books/Index");

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel model)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var book = db.Books.FirstOrDefault(x => x.Id == model.Id);
            if (book == null)
                ModelState.AddModelError("Id", "Книга не найдена");

            if (!ModelState.IsValid)
                return View(model);

            MappingBook(model, book, db);

            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Books/Index");
        }

        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var image = db.BookImages.FirstOrDefault(x => x.Id == id);
            if (image == null)
            {
                FileStream fs = System.IO.File.OpenRead(Server.MapPath(@"~/Content/Images/not-foto.png"));
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                fs.Close();

                return File(new MemoryStream(fileData), "image/jpeg");
            }

            return File(new MemoryStream(image.Data), image.ContentType);
        }

        private void MappingBook(BookViewModel sourse, Book destination, WebAppAspNetMvcDatabaseFirstEntities db)
        {
            destination.Name = sourse.Name;
            destination.Isbn = sourse.Isbn;
            destination.Year = sourse.Year;
            destination.Cost = sourse.Cost;
            destination.GenreId = sourse.GenreId;
            destination.Genre = sourse.Genre;            

            if (destination.Authors != null)
                destination.Authors.Clear();

            if (sourse.AuthorIds != null && sourse.AuthorIds.Any())
                destination.Authors = db.Authors.Where(s => sourse.AuthorIds.Contains(s.Id)).ToList();

            if (sourse.BookImageFile != null)
            {
                var image = db.BookImages.FirstOrDefault(x => x.Id == sourse.Id);
                if (image != null)
                    db.BookImages.Remove(image);

                var data = new byte[sourse.BookImageFile.ContentLength];
                sourse.BookImageFile.InputStream.Read(data, 0, sourse.BookImageFile.ContentLength);

                destination.BookImage = new BookImage()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = sourse.BookImageFile.ContentType,
                    FileName = sourse.BookImageFile.FileName
                };
            }
        }
          
        private List<BookViewModel> MappingBooks(List<Book> books)
        {
            var result = books.Select(x => new BookViewModel() { 
                   Id = x.Id,
                   Cost = x.Cost,
                   CreateAt = x.CreateAt,
                   Genre = x.Genre,
                   Isbn =x.Isbn,
                   Name = x.Name,
                   Year = x.Year,
                   GenreId = x.GenreId,   
                   BookImage = x.BookImage,
                   Authors = x.Authors
            }).ToList();

            return result;
        }
    }
}