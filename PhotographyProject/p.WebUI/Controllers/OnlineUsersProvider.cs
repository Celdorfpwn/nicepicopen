using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;

namespace p.WebUI.Controllers
{
    public class OnlineUsersProvider
    {
        private const string _name = "usersOnline";

        private static DatabaseContext Db
        {
            get
            {
                return new DatabaseContext();
            }
        }

        public static void AddOnline(int userId)
        {
            var db = Db;
            var user = db.OnlineUsers.FirstOrDefault(u => u.UserId.Equals(userId));
            if (user == null)
            {
                db.OnlineUsers.Add(new OnlineUser { UserId = userId });
                db.SaveChanges();
            }
        }

        public static void GoOffline(int userId)
        {
            var db = Db;
            var user = db.OnlineUsers.FirstOrDefault(u => u.UserId.Equals(userId));
            if (user != null)
            {
                db.OnlineUsers.Remove(user);
                db.SaveChanges();
            }
        }

        public static bool IsOnline(int userId)
        {
            var db = Db;
            var user = db.OnlineUsers.FirstOrDefault(u => u.UserId.Equals(userId));
            if (user != null)
            {
                return true;
            }
            else
                return false;
        }





    }
}