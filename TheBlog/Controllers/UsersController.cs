using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBlog.Models;

namespace TheBlog.Controllers
{
    public class UsersController : Controller
    {
        
        // GET: Users
        public ActionResult Index()
        {
            User user = Singleton.GetUser((Session["User"] as User).Username.ToString());
            return View(user);
        }

        [AcceptVerbs("GET")]
        public ActionResult Register()
        {
            Session["errors"] = null;
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Register(User u)
        {
            if (u.Username.Length > 0 && u.Password.Length > 7)
            {
                u.Id = IncrementId();
                Singleton.models.Users.Add(u);
                Singleton.models.SaveChanges();
                return Login(u);
            }
            else
            {
                Session["errors"] = "Please enter correct data";
                return View(u);
            }
        }

        [AcceptVerbs("GET")]
        public ActionResult Login()
        {
            Session["errors"] = null;
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Login(User u)
        {
            User user = Singleton.GetUser(u);

            if (user != null)
            {
                Session["errors"] = null;

                if (isAdmin(user))
                {
                    Session["IsAdmin"] = u.Password;
                }
                Session["User"] = u;
            }
            else
            {// Wrong password so send back error
                Session["errors"] = "Wrong password silly!";
                return View(u);
            }
            
            return RedirectToAction("Index", "Posts");
        }

        public ActionResult Logout()
        {
            Session["IsAdmin"] = null;
            Session["User"] = null;
            return RedirectToAction("Index", "Posts");
        }

        private int IncrementId()
        {
            if (Singleton.models.Users.Count() > 0)
            {
                return Singleton.models.Users.Max(x => x.Id) + 1;
            }
            else
            {
                return 0;
            }
        }

        private bool isAdmin(User u)
        {
            return (Singleton.models.Administrators.FirstOrDefault(x => x.Id == u.Id) != null)? true : false;
        }
    }
}