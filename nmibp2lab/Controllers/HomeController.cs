using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nmibp2lab.Models;
using nmibp2lab.Mongo;
using MongoDB.Bson;

namespace nmibp2lab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var helper = new MongoNewsHelper();
            var someNews = helper.GetNews(5);

            return View(someNews);
        }

        [HttpPost]
        public ActionResult Index(ObjectId Id, string comment)
        {
            var helper = new MongoNewsHelper();
            helper.PostComment(Id, comment);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase upload, Post post)
        {
            if(upload != null && upload.ContentLength > 0)
            {
                using( var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    post.image = reader.ReadBytes(upload.ContentLength);
                }
            }
            if(post.comments == null)
            {
                post.comments = new List<string>();
            }
            var helper = new MongoNewsHelper();
            helper.PostPost(post);

            return RedirectToAction("Index", "Home");
        }
    }
}