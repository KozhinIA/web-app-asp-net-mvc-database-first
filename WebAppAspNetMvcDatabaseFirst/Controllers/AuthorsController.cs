using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebAppAspNetMvcDatabaseFirst.Models;
using WebAppAspNetMvcDatabaseFirst.Models.Entitys;

namespace WebAppAspNetMvcDatabaseFirst.Controllers
{
    public class AuthorsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var authors = MappingAuthors(db.Authors.ToList());

            return View(authors);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var author = new AuthorViewModel();
            return View(author);
        }

        [HttpPost]
        public ActionResult Create(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new WebAppAspNetMvcDatabaseFirstEntities();

            var author = new Author();
            MappingAuthor(model, author);
            db.Authors.Add(author);
            db.SaveChanges();

            return RedirectPermanent("/Authors/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var author = db.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
                return RedirectPermanent("/Authors/Index");

            db.Authors.Remove(author);
            db.SaveChanges();

            return RedirectPermanent("/Authors/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var author = MappingAuthors(db.Authors.Where(x=> x.Id == id).ToList()).FirstOrDefault(x => x.Id == id);
            if (author == null)
                return RedirectPermanent("/Authors/Index");

            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel model)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var author = db.Authors.FirstOrDefault(x => x.Id == model.Id);
            if (author == null)
                ModelState.AddModelError("Id", "Книга не найдена");

            if (!ModelState.IsValid)
                return View(model);

            MappingAuthor(model, author);

            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Authors/Index");
        }

        private void MappingAuthor(AuthorViewModel sourse, Author destination)
        {
            destination.FirestName = sourse.FirestName;
            destination.LastName = sourse.LastName;
            destination.Birthday = sourse.Birthday;
            destination.Gender = (int)sourse.Gender;
        }

        private List<AuthorViewModel> MappingAuthors(List<Author> authors)
        {
            var result = authors.Select(x => new AuthorViewModel()
            {
                Id = x.Id,
                Birthday = x.Birthday,
                FirestName = x.FirestName,
                Gender = (Gender)x.Gender,
                LastName = x.LastName
            }).ToList();

            return result;
        }
    }
}