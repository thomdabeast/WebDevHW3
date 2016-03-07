using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBlog.Models;

namespace TheBlog.Controllers
{
    public static class Singleton
    {
        public static BlogEntities models = new BlogEntities();

        public static User GetUser(string username)
        {
            return models.Users.Where(x => x.Username == username).First();
        }

        public static User GetUser(User user)
        {
            return models.Users.FirstOrDefault(x => (x.Username == user.Username && x.Password == user.Password));
        }

        public static User GetUser(int? id)
        {
            return id.HasValue ? models.Users.Where(x => (x.Id == id)).First() : new User() { Id = -1 };
        }
    }
}