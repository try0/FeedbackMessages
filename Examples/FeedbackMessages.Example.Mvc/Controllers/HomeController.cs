using FeedbackMessages.Example.Mvc.Models;
using FeedbackMessages.Extensions;
using System.Web.Mvc;

namespace FeedbackMessages.Example.Mvc.Controllers
{
    public class HomeController : Controller
    {
 
        [HttpGet]
        public ActionResult Index()
        {
            this.InfoMessage("Information message.");
            this.SuccessMessage("Success message.");
            this.WarnMessage("Warning message.");
            this.ErrorMessage("Error message.");

            return View();
        }

        [HttpPost]
        public ActionResult Index(MessageModel model, string submitButton)
        {
            this.InfoMessage(model.Message);

            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "SecondPage":
                        return RedirectToAction("SecondPage");
                    default:
                        return View();
                }
            }

            return View(model);
        }

        public ActionResult SecondPage()
        {
            this.InfoMessage("SecondPage");
            return View();
        }

    }
}