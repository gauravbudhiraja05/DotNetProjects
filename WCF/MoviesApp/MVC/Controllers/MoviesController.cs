using MVC.Models;
using MVC.ServiceReference1;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class MoviesController : Controller
    {
        MyServiceClient mvService = new MyServiceClient();

        public ActionResult Index()
        {
            List<MoviesModel> lstRecord = new List<MoviesModel>();

            var lst = mvService.GetAllMovie();

            foreach (var item in lst)
            {
                MoviesModel mv = new MoviesModel();
                mv.Id = item.Id;
                mv.Title = item.Title;
                mv.Genre = item.Genre;
                mv.Price = item.Price;
                lstRecord.Add(mv);

            }

            return View(lstRecord);
        }

        public ActionResult Details(int id)
        {
            var lst = mvService.GetAllMovieById(id);
            MoviesModel mv = new MoviesModel();
            mv.Id = lst.Id;
            mv.Title = lst.Title;
            mv.Genre = lst.Genre;
            mv.Price = lst.Price;

            if (mv == null)
            {
                return HttpNotFound();
            }
            return View(mv);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MoviesModel mdl)
        {

            MoviesModel mv = new MoviesModel();
            mv.Title = mdl.Title;
            mv.Genre = mdl.Genre;
            mv.Price = mdl.Price;
            mvService.AddMovie(mv.Title, mv.Genre, mv.Price);
            return RedirectToAction("Index", "Movies");

        }

        public ActionResult Delete(int id)
        {
            int retval = mvService.DeleteMovieById(id);
            if (retval > 0)
            {
                return RedirectToAction("Index", "Movies");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var lst = mvService.GetAllMovieById(id);
            MoviesModel mv = new MoviesModel();
            mv.Id = lst.Id;
            mv.Title = lst.Title;
            mv.Genre = lst.Genre;
            mv.Price = lst.Price;

            if (mv == null)
            {
                return HttpNotFound();
            }
            return View(mv);

        }

        [HttpPost]
        public ActionResult Edit(MoviesModel mdl)
        {
            MoviesModel mv = new MoviesModel();
            mv.Id = mdl.Id;
            mv.Title = mdl.Title;
            mv.Genre = mdl.Genre;
            mv.Price = mdl.Price;

            int Retval = mvService.UpdateMovie(mv.Id, mv.Title, mv.Genre, mv.Price);
            if (Retval > 0)
            {
                return RedirectToAction("Index", "Movies");
            }

            return View();
        }
    }
}