using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
    public class MyService : IMyService
    {
        public void DoWork()
        {
        }

        public List<movy> GetAllMovie()
        {
            List<movy> movieList = new List<movy>();
            MoviesDbEntities tstDb = new MoviesDbEntities();
            var movieListResult = from k in tstDb.movies select k;
            foreach (var item in movieListResult)
            {
                movy mv = new movy();
                mv.Id = item.Id;
                mv.Title = item.Title;
                mv.Genre = item.Genre;
                mv.Price = item.Price;
                movieList.Add(mv);
            }

            return movieList;
        }

        public movy GetAllMovieById(int id)
        {

            MoviesDbEntities tstDb = new MoviesDbEntities();
            var movieListResult = from k in tstDb.movies where k.Id == id select k;
            movy mv = new movy();
            foreach (var item in movieListResult)
            {
                mv.Id = item.Id;
                mv.Title = item.Title;
                mv.Genre = item.Genre;
                mv.Price = item.Price;
            }

            return mv;
        }

        public int DeleteMovieById(int Id)
        {

            MoviesDbEntities tstDb = new MoviesDbEntities();
            movy mvd = new movy();
            mvd.Id = Id;
            tstDb.Entry(mvd).State = EntityState.Deleted;
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int AddMovie(string Title, string Genre, string Price)
        {
            MoviesDbEntities tstDb = new MoviesDbEntities();
            movy mvd = new movy();
            mvd.Title = Title;
            mvd.Genre = Genre;
            mvd.Price = Price;
            tstDb.movies.Add(mvd);
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int UpdateMovie(int Id, string Title, string Genre, string Price)
        {
            MoviesDbEntities tstDb = new MoviesDbEntities();
            movy mvd = new movy();
            mvd.Id = Id;
            mvd.Title = Title;
            mvd.Genre = Genre;
            mvd.Price = Price;
            tstDb.Entry(mvd).State = EntityState.Modified;

            int Retval = tstDb.SaveChanges();
            return Retval;
        }
    }
}
