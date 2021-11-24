using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebAppAspNetMvcDatabaseFirst.Models;
using WebAppAspNetMvcDatabaseFirst.Models.Entitys;

namespace WebAppAspNetMvcDatabaseFirst.Controllers
{
    public class GenresController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var genres = MappingGenres(db.Genres.ToList());

            return View(genres);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var genre = new GenreViewModel();
            return View(genre);
        }

        [HttpPost]
        public ActionResult Create(GenreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var genre = new Genre();
            MappingGenre(model, genre);
            db.Genres.Add(genre);
            db.SaveChanges();

            return RedirectPermanent("/Genres/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var genre = db.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
                return RedirectPermanent("/Genres/Index");

            db.Genres.Remove(genre);
            db.SaveChanges();

            return RedirectPermanent("/Genres/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var genre = MappingGenres(db.Genres.Where(x => x.Id == id).ToList()).FirstOrDefault(x => x.Id == id);
            if (genre == null)
                return RedirectPermanent("/Genres/Index");

            return View(genre);
        }

        [HttpPost]
        public ActionResult Edit(GenreViewModel model)
        {
            var db = new WebAppAspNetMvcDatabaseFirstEntities();
            var genre = db.Genres.FirstOrDefault(x => x.Id == model.Id);
            if (genre == null)
                ModelState.AddModelError("Id", "Жанр не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingGenre(model, genre);

            db.Entry(genre).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Genres/Index");
        }

        private void MappingGenre(GenreViewModel sourse, Genre destination)
        {
            destination.Name = sourse.Name;
        }

        private List<GenreViewModel> MappingGenres(List<Genre> genres)
        {
            var result = genres.Select(x => new GenreViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return result;
        }
    }
}