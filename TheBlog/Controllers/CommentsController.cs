using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBlog.Models;

namespace TheBlog.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("GET")]
        public ActionResult Create(int pid)
        {
            return View(new Comment() { PostID = pid });
        }

        [AcceptVerbs("POST")]
        public ActionResult Create(Comment p)
        {
            p.Id = IncrementId();
            p.Name = (Session["user"] as User).Username.ToString();
            p.Date = DateTime.Now.ToString();
            p.UserID = Singleton.GetUser((Session["user"] as User).Username).Id;

            Singleton.models.Comments.Add(p);
            Singleton.models.SaveChanges();
            return RedirectToAction("See", "Posts", new { id = p.PostID });
        }

        [AcceptVerbs("GET")]
        public ActionResult Edit(int id)
        {
            return View(Singleton.models.Comments.FirstOrDefault(x => x.Id == id));
        }

        [AcceptVerbs("POST")]
        public ActionResult Edit(Comment c)
        {
            Comment temp = Singleton.GetComment(c.Id);
            string body = c.Body;
            c = temp;
            c.Body = body;

            Singleton.models.Comments.Remove(temp);
            Singleton.models.SaveChanges();
            Singleton.models.Comments.Add(c);
            Singleton.models.SaveChanges();
            return RedirectToAction("See", "Posts", new { id = c.PostID });
        }

        public ActionResult Delete(int id)
        {
            Comment c = Singleton.GetComment(id);
            int postId = c.PostID;

            Singleton.models.Comments.Remove(c);
            Singleton.models.SaveChanges();
            return RedirectToAction("See", "Posts", new { id = postId });
        }

        private int IncrementId()
        {
            if (Singleton.models.Comments.Count() > 0)
            {
                return Singleton.models.Comments.Max(x => x.Id) + 1;
            }
            else
            {
                return 0;
            }
        }
    }
}