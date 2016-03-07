using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBlog.Models;

namespace TheBlog.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            IEnumerable<Post> posts = from post in Singleton.models.Posts orderby post.Date descending select post;
            ViewBag.IsAdmin = IsAdmin;
            return View(posts);
        }

        public ActionResult Update(int? id, string title, string body, DateTime dateTime)
        {
            if (!IsAdmin)
            {
                return RedirectToAction("Index");
            }

            Post post = GetPost(id);
            post.Title = title;
            post.Body = body;
            post.Date = dateTime;

            if (!id.HasValue)
            {
                Singleton.models.Posts.Add(post);
            }
            Singleton.models.SaveChanges();

            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET")]
        public ActionResult Edit(int? id)
        {
            Post post = GetPost(id);
            return View(post);
        }

        [AcceptVerbs("POST")]
        public ActionResult Edit(Post p)
        {
            Post temp = GetPost(p.Id);
            p.UserId = temp.UserId;
            Singleton.models.Posts.Remove(temp);
            Singleton.models.Posts.Add(p);

            Singleton.models.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            Singleton.models.Posts.Remove(GetPost(id));
            Singleton.models.SaveChanges();
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET")]
        public ActionResult Create()
        {

            Post p = new Post();
            return View(p);
        }

        [AcceptVerbs("POST")]
        public ActionResult Create(Post p)
        {
            p.Id = IncrementId();
            p.UserId = Singleton.GetUser((Session["user"] as User).Username.ToString()).Id;

            Singleton.models.Posts.Add(p);
            Singleton.models.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult See(int? id)
        {
            Post p = GetPost(id);
            if (p.Id != -1)
            {
                Session["poster"] = Singleton.GetUser(p.UserId).Username;
                return View(p);
            }
            return RedirectToAction("Index");
        }

        private int IncrementId()
        {
            if (Singleton.models.Posts.Count() > 0)
            {
                return Singleton.models.Posts.Max(x => x.Id) + 1;
            }
            else
            {
                return 0;
            }
        }

        private Post GetPost(Post p)
        {
            return Singleton.models.Posts.Where(x => x.Equals(p)).First();
        }

        private Post GetPost(int? id)
        {
            return id.HasValue ? Singleton.models.Posts.Where(x => x.Id == id).First() : new Post() { Id = -1 };
        }

        public bool IsAdmin { get { return true; /* Session["IsAdmin"] != null && (bool)Session["IsAdmin"]; */ } }
    }
}