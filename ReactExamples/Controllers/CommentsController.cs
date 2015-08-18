using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReactExamples.Controllers
{
    public class CommentsController : Controller
    {
        private IDocumentSession _documentSession;
        public CommentsController()
        {
            var documentStore = new DocumentStore { ConnectionStringName = "localhost" }.Initialize();
            _documentSession = documentStore.OpenSession();
        }

        //
        // GET: /Comments/
        public ActionResult Index()
        {
            var comments = _documentSession.Query<Comment>().Customize(x => x.WaitForNonStaleResults()).ToList();
            
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult AddComment(CommentViewModel model)
        {
            Comment comment = new Comment() {author = model.Author, text = model.Text};
            _documentSession.Store(comment);
            _documentSession.SaveChanges();
            //return Json(comment);
            return Json(_documentSession.Query<Comment>().Customize(x => x.WaitForNonStaleResults()).ToList(),
                JsonRequestBehavior.AllowGet);
        }
    }
}