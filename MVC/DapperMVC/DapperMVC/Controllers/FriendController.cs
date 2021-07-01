using Dapper;
using DapperMVC.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace DapperMVC.Controllers
{
    public class FriendController : Controller
    {
        IDbConnection _dbConnection;
        public FriendController()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["friendConnection"].ConnectionString);
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
        }

        // GET: Friend
        public ActionResult Index()
        {
            List<Friend> friendList = _dbConnection.Query<Friend>("Select * From tblFriends").ToList();
            return View(friendList);
        }

        // GET: Friend/Details/5
        public ActionResult Details(int id)
        {
            Friend friend = _dbConnection.Query<Friend>("Select * From tblFriends " +
                                     "WHERE FriendID =" + id, new { id }).SingleOrDefault();
            return View(friend);
        }

        // GET: Friend/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Friend/Create
        [HttpPost]
        public ActionResult Create(Friend friend)
        {
            try
            {
                string sqlQuery = "Insert Into tblFriends (FriendName,City,PhoneNumber) Values('" + friend.FriendName + "','" + friend.City + "','" + friend.PhoneNumber + "')";
                int rowsAffected = _dbConnection.Execute(sqlQuery);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Friend/Edit/5
        public ActionResult Edit(int id)
        {
            Friend friend = _dbConnection.Query<Friend>("Select * From tblFriends " +
                                    "WHERE FriendID =" + id, new { id }).SingleOrDefault();
            return View(friend);
        }

        // POST: Friend/Edit/5
        [HttpPost]
        public ActionResult Edit(Friend friend)
        {
            try
            {
                string sqlQuery = "update tblFriends set FriendName='" + friend.FriendName + "',City='" + friend.City + "',PhoneNumber='" + friend.PhoneNumber + "' where friendid=" + friend.FriendID;
                int rowsAffected = _dbConnection.Execute(sqlQuery);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Friend/Delete/5
        public ActionResult Delete(int id)
        {
            Friend friend = _dbConnection.Query<Friend>("Select * From tblFriends " +
                                     "WHERE FriendID =" + id, new { id }).SingleOrDefault();
            return View(friend);
        }

        // POST: Friend/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string sqlQuery = "Delete From tblFriends WHERE FriendID = " + id;
                int rowsAffected = _dbConnection.Execute(sqlQuery);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
